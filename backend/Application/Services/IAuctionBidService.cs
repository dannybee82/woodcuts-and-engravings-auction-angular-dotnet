using Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IAuctionBidService
    {

        Task<bool> PlaceBid(int auctionItemId, int userId, decimal amount);

        Task<List<AuctionBidDetailDto>> GetLatestBids(int auctionId);

    }
}
