using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Transactions
{
    public class AuctionDeleteTransaction : IAuctionDeleteTransaction
    {
        private readonly AppDbContext _dbContext;

        public AuctionDeleteTransaction(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(int auctionItemId)
        {
            try
            {
                using var tx = await _dbContext.Database.BeginTransactionAsync();

                var record = await _dbContext.AuctionItems.AsNoTracking()
                    .Include(x => x.Detail)
                    .Include(x => x.Image)
                    .Include(x => x.Bids.OrderByDescending(x => x.Amount).ThenBy(x => x.Id).ThenBy(x => x.PlacedAt).Take(5))
                    .SingleOrDefaultAsync(x => x.Id == auctionItemId)
                    .ConfigureAwait(false);

                if (record == null)
                {
                    throw new Exception("Record not found.");
                }

                // Delete details
                _dbContext.AuctionItemDetails.Remove(record.Detail);

                // Delete image
                _dbContext.AuctionItemImages.Remove(record.Image);

                // Delete from AuctionSoldItems table
                var recordWonItem = await _dbContext.AuctionWonItems.AsNoTracking()
                    .SingleOrDefaultAsync(x => x.AuctionItemId == auctionItemId)
                    .ConfigureAwait(false);

                if(recordWonItem != null)
                {
                    _dbContext.AuctionWonItems.Remove(recordWonItem);
                }

                // Delete Auction self - Note: For the Auction Bids cascading delete is enabled. 
                _dbContext.AuctionItems.Remove(record);

                await _dbContext.SaveChangesAsync();
                await tx.CommitAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Can't delete Auction: " + ex.Message);
            }
        }
    }
}
