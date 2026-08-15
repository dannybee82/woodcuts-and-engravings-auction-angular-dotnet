using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class AuctionItemImageRepository : IAuctionItemImageRepository
    {
        private readonly AppDbContext _dbContext;

        public AuctionItemImageRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Delete(int id)
        {
            var record = await GetById(id);

            if (record != null)
            {
                _dbContext.AuctionItemImages.Remove(record);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<AuctionItemImage?> GetById(int id)
        {
            return await _dbContext.AuctionItemImages
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
        }

        public async Task Update(AuctionItemImage entity)
        {
            _dbContext.AuctionItemImages.Update(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
