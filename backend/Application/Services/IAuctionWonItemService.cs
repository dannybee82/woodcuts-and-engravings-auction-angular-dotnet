using Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IAuctionWonItemService
    {

        Task<List<AuctionItemDto>> GetAllWonItems(int userId);

    }
}
