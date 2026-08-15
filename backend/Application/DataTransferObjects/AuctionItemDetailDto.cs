using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransferObjects
{
    public class AuctionItemDetailDto
    {
        public int? Id { get; set; }
        public string Title { get; set; }

        public string Description { get; set; }

        public string ItemType { get; set; }
    }
}
