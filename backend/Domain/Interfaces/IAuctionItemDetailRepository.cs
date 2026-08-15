using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAuctionItemDetailRepository
    {

        Task<AuctionItemDetail?> GetById(int id);

        Task Update(AuctionItemDetail entity);

        Task Delete(int id);

    }
}
