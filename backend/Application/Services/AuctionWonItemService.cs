using Application.DataTransferObjects;
using Application.Mappers;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class AuctionWonItemService : IAuctionWonItemService
    {
        private readonly IAuctionWonItemRepository _auctionWonItemRepository;

        public AuctionWonItemService(IAuctionWonItemRepository auctionWonItemRepository)
        {
            _auctionWonItemRepository = auctionWonItemRepository;
        }

        public async Task<List<AuctionItemDto>> GetAllWonItems(int userId)
        {
            var records = await _auctionWonItemRepository.GetQueryable()
                .Include(x => x.Item)
                .ThenInclude(y => y.Detail)
                .Include(x => x.Item)
                .ThenInclude(y => y.Image)
                .Include(x => x.User)
                .Where(x => x.UserId == userId)
                .OrderByDescending(x => x.Id)                
                .ToListAsync()
                .ConfigureAwait(false);

            return records.Count() == 0 ? 
                new List<AuctionItemDto>() :
                records.Select(x => AuctionItemMapper.ToDto(x.Item)).ToList();
        }
    }
}
