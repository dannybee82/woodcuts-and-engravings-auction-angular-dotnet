using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AuctionWonItemRepository : IAuctionWonItemRepository
    {
        private readonly AppDbContext _dbContext;

        public AuctionWonItemRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Create(AuctionWonItem entity)
        {
            await _dbContext.AuctionWonItems.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
        }

        public async Task Delete(int auctionItemId)
        {
            var record = await _dbContext.AuctionWonItems
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.AuctionItemId == auctionItemId)
                .ConfigureAwait(false);

            if(record != null)
            {
                _dbContext.AuctionWonItems.Remove(record);
                await _dbContext.SaveChangesAsync();
            }
        }

        public IQueryable<AuctionWonItem> GetQueryable()
        {
            return _dbContext.AuctionWonItems
                .AsNoTracking();
        }
    }
}
