using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAuctionBidTransaction
    {

        Task<AuctionItem?> PlaceBid(int auctionItemId, int userId, decimal amount);
    }

}
