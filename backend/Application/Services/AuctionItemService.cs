using Application.BackgroundJobs;
using Application.DataTransferObjects;
using Application.Mappers;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Domain.Paging;
using Domain.Settings;
using FileSignatures;
using Hangfire;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class AuctionItemService : IAuctionItemService
    {
        private readonly IAuctionItemRepository _auctionItemRepository;
        private readonly IAuctionItemDetailRepository _auctionItemDetailRepository;
        private readonly IAuctionItemImageRepository _auctionItemImageRepository;
        private readonly IAuctionWonItemRepository _auctionWonItemRepository;
        private readonly IAuctionDeleteTransaction _auctionDeleteTransaction;
        private readonly IFileFormatInspector _fileFormatInspector;
        private readonly IAllowedFileFormatList _allowedFileFormatList;
        private readonly IServiceProvider _services;

        public AuctionItemService(
            IAuctionItemRepository auctionItemRepository,             
            IAuctionItemDetailRepository auctionItemDetailRepository,
            IAuctionItemImageRepository auctionItemImageRepository,
            IAuctionWonItemRepository auctionWonItemRepository,
            IAuctionDeleteTransaction auctionDeleteTransaction,
            IFileFormatInspector fileFormatInspector,
            IAllowedFileFormatList allowedFileFormatList,
            IServiceProvider services
        )
        {
            _auctionItemRepository = auctionItemRepository;
            _auctionItemImageRepository = auctionItemImageRepository;            
            _auctionItemDetailRepository = auctionItemDetailRepository;
            _auctionWonItemRepository = auctionWonItemRepository;
            _auctionDeleteTransaction = auctionDeleteTransaction;
            _fileFormatInspector = fileFormatInspector;
            _allowedFileFormatList = allowedFileFormatList;
            _services = services;
        }

        public async Task CreateAuction(IFormFile file, CreateOrUpdateAuctionDto dto)
        {
            if (file == null)
            {
                throw new Exception("File is required");
            }
        
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);

            var fileFormat = _fileFormatInspector.DetermineFileFormat(ms);
            string extension = Path.GetExtension(file.FileName).ToLower().Replace(".", "");

            if(fileFormat == null || string.IsNullOrEmpty(extension))
            {
                throw new Exception("Unable to determine fileformat or extension.");
            }

            var record = _allowedFileFormatList.AllowedFileList
                .SingleOrDefault(x =>
                    x.Extension.Equals(extension) &&
                    x.FileFormat.Any(y => y.GetType() == fileFormat.GetType()));

            if (record == null)
            {
                throw new Exception("Not allowed file format.");
            }

            var entity = AuctionItemMapper.ToEntity(dto, ms.ToArray(), record.Extension);
            var item = await _auctionItemRepository.Create(entity);

            // Scheldule Auction.
            using var scope = _services.CreateScope();
            var scheduler = scope.ServiceProvider.GetRequiredService<IBackgroundJobScheduler>();
            scheduler.Schedule<AuctionCloser>(x => x.CloseAuction(item.Id), item.EndsAt);
        }

        public async Task DeleteAuction(int id)
        {
            await _auctionDeleteTransaction.Delete(id);
        }

        public async Task<List<AuctionItemDto>> GetAll()
        {
            // Exclude won/sold items.
            List<int> wonItems = await _auctionWonItemRepository                
                .GetQueryable()
                .Select(x => x.AuctionItemId)
                .ToListAsync()
                .ConfigureAwait(false);

            var records = await _auctionItemRepository
                .GetQueryable()
                .Where(x => !wonItems.Contains(x.Id))
                .OrderByDescending(x => x.Id)
                .ToListAsync()
                .ConfigureAwait(false);

            return records.Select(x => AuctionItemMapper.ToDto(x)).ToList();
        }

        public async Task<AuctionItemDto> GetById(int id)
        {
            var item = await _auctionItemRepository.GetDetailsById(id);

            if(item == null)
            {
                throw new Exception("Record not found");
            }

            return AuctionItemMapper.ToDto(item);
        }

        public async Task<AuctionItemImage?> GetImage(int id)
        {
            var image = await _auctionItemImageRepository.GetById(id);
            return image;
        }

        public async Task<PaginatedListDto<AuctionItemDto>> GetPagedList(int pageNumber, int pageSize, AuctionItemFilterDto? filters)
        {
            var query = _auctionItemRepository.GetQueryable();
            bool isOrderingApplied = false;

            if(filters != null)
            {
                if(filters.IsPriceAscending)
                {
                    query = query.OrderBy(x => x.CurrentPrice);
                    isOrderingApplied = true;
                }

                if(filters.IsPriceDescending)
                {
                    query = query.OrderByDescending(x => x.CurrentPrice);
                    isOrderingApplied = true;
                }

                if (filters.IsAmountOfBidsAscending)
                {
                    query = query.OrderBy(x => x.BidCount);
                    isOrderingApplied = true;
                }

                if (filters.IsAmountOfBidsDescending)
                {
                    query = query.OrderByDescending(x => x.BidCount);
                    isOrderingApplied = true;
                }


                if (filters.ClosingSoon)
                {
                    query = query.OrderBy(e => e.EndsAt);
                    isOrderingApplied = true;
                }

                if (!string.IsNullOrEmpty(filters.FilterType))
                {
                    ItemType? type = AuctionItemDetailMapper.GetItemType(filters.FilterType);

                    if(type != null) {
                        query = query.Where(x => x.Detail.ItemType == type);
                    }
                }

                if(!string.IsNullOrEmpty(filters.TextSearch?.Trim()))
                {
                    string escaped = Regex.Replace(filters.TextSearch.Trim(), "<.*?>", String.Empty);
                    query = query.Where(x => EF.Functions.ILike(x.Detail.Title, $"%{escaped}%") || EF.Functions.ILike(x.Detail.Description, $"%{escaped}%"));
                }
            }

            if(!isOrderingApplied)
            {
                query = query.OrderByDescending(x => x.Id);
            }

            query = query.Where(x => x.Status == AuctionStatus.OPEN);

            var paginatedList = await _auctionItemRepository.GetPaginatedList(query, pageNumber, pageSize);
            return AuctionItemMapper.ToPaginatedDto(paginatedList);
        }

        public async Task UpdateAuction(IFormFile? file, CreateOrUpdateAuctionDto dto)
        {
            if(dto.Id == null)
            {
                throw new Exception("Can't update - there is no Id.");
            }

            var recordAuction = await _auctionItemRepository.GetById(dto.Id ?? 0);

            if(recordAuction == null)
            {
                throw new Exception("Can't update - record not found.");
            }

            // Update image when there is one.
            if(file != null)
            {
                using var ms = new MemoryStream();
                await file.CopyToAsync(ms);

                var fileFormat = _fileFormatInspector.DetermineFileFormat(ms);
                string extension = Path.GetExtension(file.FileName).ToLower().Replace(".", "");

                if (fileFormat == null || string.IsNullOrEmpty(extension))
                {
                    throw new Exception("Unable to determine fileformat or extension.");
                }

                var record = _allowedFileFormatList.AllowedFileList
                    .SingleOrDefault(x =>
                        x.Extension.Equals(extension) &&
                        x.FileFormat.Any(y => y.GetType() == fileFormat.GetType()));

                if (record == null)
                {
                    throw new Exception("Not allowed file format.");
                }

                var image = await _auctionItemImageRepository.GetById(recordAuction.AuctionItemImageId ?? 0);

                if(image != null)
                {
                    image.ContentType = $"image/{extension}";
                    image.Data = ms.ToArray();

                    await _auctionItemImageRepository.Update(image);
                }
            }

            // Update details.
            var details = await _auctionItemDetailRepository.GetById(recordAuction.AuctionItemDetailId);

            if(details != null)
            {
                details.Title = dto.Details.Title;
                details.Description = dto.Details.Description;
                ItemType? type = AuctionItemDetailMapper.GetItemType(dto.Details.ItemType);
                details.ItemType = type != null ? (ItemType)type : ItemType.UNKNOWN;
                await _auctionItemDetailRepository.Update(details);
            }

            // Update entity itself.
            bool rescheldule = false;
            recordAuction.Status = dto.IsOpen ? AuctionStatus.OPEN : AuctionStatus.CLOSED;

            if(dto.AuctionLength != recordAuction.AuctionLength)
            {
                var now = DateTimeOffset.UtcNow;
                recordAuction.EndsAt = now.AddDays(dto.AuctionLength);
                recordAuction.AuctionLength = dto.AuctionLength;
                rescheldule = true;
            }

            if(recordAuction.BidCount == 0)
            {
                recordAuction.CurrentPrice = dto.CurrentPrice;
            }

            var item = await _auctionItemRepository.Update(recordAuction);

            if(rescheldule)
            {
                // Rescheldule Auction.
                using var scope = _services.CreateScope();
                var scheduler = scope.ServiceProvider.GetRequiredService<IBackgroundJobScheduler>();
                scheduler.Schedule<AuctionCloser>(x => x.CloseAuction(item.Id), item.EndsAt);
            }
        }
    }
}
