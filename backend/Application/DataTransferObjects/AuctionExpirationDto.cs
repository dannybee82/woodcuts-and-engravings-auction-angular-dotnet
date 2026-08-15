using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransferObjects
{
    public class AuctionExpirationDto
    {

        public string ExpirationText { get; set; }

        public bool ExpiresSoon { get; set; }

        public int CountDownSeconds { get; set; }
    }
}
