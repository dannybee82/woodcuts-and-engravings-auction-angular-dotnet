using Domain.Entities;
using Domain.Interfaces;
using Domain.Paging;
using Infrastructure.Context;
using Infrastructure.Pagination;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AuctionItemRepository : IAuctionItemRepository
    {
        private readonly AppDbContext _dbContext;

        public AuctionItemRepository(AppDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

        public async Task<AuctionItem> Create(AuctionItem entity)
        {
            await _dbContext.AuctionItems.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Delete(int id)
        {
            var record = await GetById(id);

            if (record != null)
            {
                _dbContext.AuctionItems.Remove(record);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<AuctionItem?> GetById(int id)
        {
            return await _dbContext.AuctionItems
                .AsNoTracking()
                .SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<AuctionItem?> GetDetailsById(int id)
        {
            return await _dbContext.AuctionItems
                .AsNoTracking()
                .Include(x => x.Detail)
                .Include(x => x.Image)
                .Include(x => x.Bids.OrderByDescending(x => x.Amount).ThenBy(x => x.PlacedAt).ThenBy(x => x.Id).Take(5))
                .ThenInclude(y => y.User)
                .SingleOrDefaultAsync(x => x.Id == id)
                .ConfigureAwait(false);
        }

        public async Task<PaginatedList<AuctionItem>> GetPaginatedList(IQueryable<AuctionItem> query, int pageNumber, int pageSize)
        {
            return await PaginatedListFactory.CreateAsync(query, pageNumber, pageSize);
        }

        public IQueryable<AuctionItem> GetQueryable()
        {
            return _dbContext.AuctionItems
                .AsNoTracking()
                .Include(x => x.Detail);
        }

        public async Task<AuctionItem> Update(AuctionItem entity)
        {
            _dbContext.AuctionItems.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
