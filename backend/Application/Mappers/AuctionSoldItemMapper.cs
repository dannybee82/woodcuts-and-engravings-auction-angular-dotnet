using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public class AuctionSoldItemMapper
    {

        public static AuctionWonItem ToEntity(int itemId, decimal price, int userId)
        {
            return new AuctionWonItem()
            {
                AuctionItemId = itemId,
                UserId = userId,
                Price = price,
            };
        }

    }
}
