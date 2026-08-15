using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAuctionWonItemRepository
    {

        IQueryable<AuctionWonItem> GetQueryable();

        Task Create(AuctionWonItem entity);

        Task Delete(int auctionItemId);

    }
}
