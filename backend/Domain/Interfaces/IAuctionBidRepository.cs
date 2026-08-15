using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAuctionBidRepository
    {

        Task<List<AuctionBid>> GetLatestBids(int auctionId);

    }
}
