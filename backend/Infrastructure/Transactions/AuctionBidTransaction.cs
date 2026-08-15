using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Transactions
{
    public class AuctionBidTransaction : IAuctionBidTransaction
    {
        private readonly AppDbContext _dbContext;

        public AuctionBidTransaction(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AuctionItem?> PlaceBid(int auctionItemId, int userId, decimal amount)
        {
            try
            {
                using var tx = await _dbContext.Database.BeginTransactionAsync();

                var item = await _dbContext.AuctionItems.FirstAsync(i => i.Id == auctionItemId);

                if (DateTimeOffset.UtcNow >= item.EndsAt)
                    throw new Exception("Auction Ended");
                if (amount <= item.CurrentPrice)
                    throw new Exception("Bid is too low");

                // Add new bid.
                _dbContext.AuctionBids.Add(new AuctionBid()
                {
                    AuctionItemId = auctionItemId,
                    UserId = userId,
                    Amount = amount,
                    PlacedAt = DateTimeOffset.UtcNow
                });

                item.CurrentPrice = amount;
                item.BidCount += 1;

                // RowVersion causes DbUpdateConcurrencyException if another  
                // bid modified the row first -> catch, reload, retry/reject  
                await _dbContext.SaveChangesAsync();
                await tx.CommitAsync();

                return item;
            }
            catch (Exception ex)
            {
                // Do not save anything at this point.
                //throw;
                return null;
            }
        }
    }
}
