using Application.DataTransferObjects;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public class AuctionItemDetailMapper
    {
        private static Dictionary<ItemType, string> translations = new Dictionary<ItemType, string>()
        { 
            {ItemType.UNKNOWN, "UNKNOWN"},
            {ItemType.WOODCUT, "WOODCUT"},
            {ItemType.ENGRAVING, "ENGRAVING"},
        };

        private static Dictionary<string, ItemType> availableItemTypes = new Dictionary<string, ItemType>()
        {
            { "UNKNOWN", ItemType.UNKNOWN },
            { "WOODCUT", ItemType.WOODCUT },
            { "ENGRAVING", ItemType.ENGRAVING },
        };

        public static AuctionItemDetailDto ToDto(AuctionItemDetail entity)
        {
            string? type = string.Empty;
            bool isFound = translations.TryGetValue(entity.ItemType, out type);
            type = isFound ? type : "UNKNOWN";

            return new AuctionItemDetailDto()
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                ItemType = type
            };
        }

        public static ItemType? GetItemType(string itemType)
        {
            ItemType type;
            bool isFound = availableItemTypes.TryGetValue(itemType, out type);
            return isFound ? type : null;
        }

    }
}
