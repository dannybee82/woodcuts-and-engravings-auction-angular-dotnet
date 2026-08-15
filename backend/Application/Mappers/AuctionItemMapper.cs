using Application.DataTransferObjects;
using Domain.Entities;
using Domain.Enums;
using Domain.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public class AuctionItemMapper
    {

        public static AuctionItemDto ToDto(AuctionItem entity)
        {
            var expiration = CalculateExpirationTime(entity.EndsAt);

            return new AuctionItemDto()
            {
                Id = entity.Id,
                IsOpen = entity.Status == AuctionStatus.OPEN ? true : false,
                Details = AuctionItemDetailMapper.ToDto(entity.Detail),
                ImageId = entity.AuctionItemImageId,                 
                CurrentPrice = entity.CurrentPrice,
                BidCount = entity.BidCount,
                EndsAt = entity.EndsAt,
                AuctionLength = entity.AuctionLength,
                Expiration = CalculateExpirationTime(entity.EndsAt)
            };
        }

        public static PaginatedListDto<AuctionItemDto> ToPaginatedDto(PaginatedList<AuctionItem> paginatedList)
        {
            return new PaginatedListDto<AuctionItemDto>()
            {                
                TotalCount = paginatedList.TotalCount,
                CurrentPage = paginatedList.CurrentPage,
                PageSize = paginatedList.PageSize,
                TotalPages = paginatedList.TotalPages,
                From = paginatedList.From,
                To = paginatedList.To,
                Items = paginatedList.Items.Select(x => ToDto(x)).ToList()
            };
        }

        public static AuctionItem ToEntity(CreateOrUpdateAuctionDto dto, byte[] image, string fileFormat)
        {
            AuctionItem entity = new();

            if(dto.Id != null)
            {
                entity.Id = dto.Id ?? 0;
            }
            entity.Status = dto.IsOpen ? AuctionStatus.OPEN : AuctionStatus.CLOSED;

            AuctionItemDetail details = new();
            
            if(dto.Details.Id != null)
            {
                details.Id = dto.Details.Id ?? 0;
            }
            details.Title = dto.Details.Title;
            details.Description = dto.Details.Description;
            ItemType? type = AuctionItemDetailMapper.GetItemType(dto.Details.ItemType);
            details.ItemType = type != null ? (ItemType) type : ItemType.UNKNOWN;
            entity.Detail = details;

            entity.CurrentPrice = dto.CurrentPrice;
            entity.AuctionLength = dto.AuctionLength;
            entity.CreatedAt = DateTime.UtcNow;

            var now = DateTimeOffset.UtcNow;
            entity.EndsAt = now.AddDays(dto.AuctionLength);

            entity.Image = new AuctionItemImage()
            {
                ContentType = $"image/{fileFormat}",
                Data = image
            };

            return entity;
        }

        private static AuctionExpirationDto CalculateExpirationTime(DateTimeOffset endsAt)
        {
            AuctionExpirationDto dto = new AuctionExpirationDto()
            {
                ExpirationText = string.Empty,
                ExpiresSoon = false,
                CountDownSeconds = -1
            };

            var currentDate = DateTimeOffset.UtcNow;
            var dateDays = (endsAt - currentDate);

            if (dateDays.Days >= 1)
            {
                dto.ExpirationText = dateDays.Days > 1 ? $"{dateDays.Days} days" : $"{dateDays.Days} day";
                return dto;
            }
            if (dateDays.Hours >= 1)
            {
                dto.ExpirationText = dateDays.Hours > 1 ? $"{dateDays.Hours} hours" : $"{dateDays.Hours} hour";
                return dto;
            }
            if (dateDays.Minutes >= 1)
            {
                dto.ExpiresSoon = true;
                dto.CountDownSeconds = (int)dateDays.TotalSeconds;
                dto.ExpirationText = dateDays.Minutes > 1 ? $"{dateDays.Minutes} minutes" : $"{dateDays.Minutes} minute";
                return dto;
            }
            if (dateDays.Seconds >= 1)
            {
                dto.ExpiresSoon = true;
                dto.CountDownSeconds = (int)dateDays.TotalSeconds;
                dto.ExpirationText = dateDays.Seconds > 1 ? $"{dateDays.Seconds} seconds" : $"{dateDays.Seconds} second";
                return dto;
            }

            return dto;
        }

    }
}
