using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
    public interface IAuctionItemImageRepository
    {

        Task<AuctionItemImage?> GetById(int id);

        Task Update(AuctionItemImage entity);

        Task Delete(int id);
    }
}
