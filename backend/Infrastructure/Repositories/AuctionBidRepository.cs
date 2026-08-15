using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AuctionBidRepository : IAuctionBidRepository
    {
        private readonly AppDbContext _dbContext;

        public AuctionBidRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<AuctionBid>> GetLatestBids(int auctionId)
        {
            return await _dbContext.AuctionBids
                .Include(x => x.User)
                .Where(x => x.AuctionItemId == auctionId)                
                .OrderByDescending(x => x.Amount)
                .ThenBy(x => x.Id)
                .ThenBy(x => x.PlacedAt)                
                .Take(5)
                .ToListAsync()
                .ConfigureAwait(false);
        }
    }
}
