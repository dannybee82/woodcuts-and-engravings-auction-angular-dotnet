using Domain.Entities;
using Domain.Paging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAuctionItemRepository
    {
        IQueryable<AuctionItem> GetQueryable();

        Task<PaginatedList<AuctionItem>> GetPaginatedList(IQueryable<AuctionItem> query, int pageNumber, int pageSize);

        Task<AuctionItem?> GetDetailsById(int id);

        Task<AuctionItem?> GetById(int id);

        Task<AuctionItem> Create(AuctionItem entity);

        Task<AuctionItem> Update(AuctionItem entity);

        Task Delete(int id);
    }
}
