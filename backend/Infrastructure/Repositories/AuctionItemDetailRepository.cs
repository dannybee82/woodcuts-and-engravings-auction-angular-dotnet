using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AuctionItemDetailRepository : IAuctionItemDetailRepository
    {
        private readonly AppDbContext _dbContext;

        public AuctionItemDetailRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(int id)
        {
            var record = await GetById(id);

            if(record != null)
            {
                _dbContext.AuctionItemDetails.Remove(record);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<AuctionItemDetail?> GetById(int id)
        {
            return await _dbContext.AuctionItemDetails
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
        }

        public async Task Update(AuctionItemDetail entity)
        {
            _dbContext.AuctionItemDetails.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
