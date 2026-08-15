using Application.DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IHelpersService
    {

        Task<string> SimulateBid(HelperSimulateBidDto dto);

        Task<string> EndAuctionSoon(int auctionItemId);

    }
}
