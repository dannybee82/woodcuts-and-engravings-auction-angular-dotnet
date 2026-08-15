using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransferObjects
{
    public class AuctionItemDto
    {
        public int Id { get; set; }

        public bool IsOpen { get; set; }

        public AuctionItemDetailDto Details { get; set; }

        public int ImageId { get; set; }

        public decimal CurrentPrice { get; set; }
        public int BidCount { get; set; }

        public DateTimeOffset EndsAt { get; set; }

        public int AuctionLength { get; set; }

        public AuctionExpirationDto Expiration { get; set; }
    }
}
