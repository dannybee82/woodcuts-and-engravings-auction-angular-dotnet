using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransferObjects
{
    public class PlaceBidDto
    {

        public int AuctionItemId { get; set; }

        public int UserId { get; set; }

        public decimal Amount { get; set; }

    }
}
