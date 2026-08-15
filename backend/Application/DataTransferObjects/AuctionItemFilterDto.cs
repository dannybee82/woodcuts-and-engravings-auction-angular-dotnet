using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DataTransferObjects
{
    public class AuctionItemFilterDto
    {
        public bool IsPriceAscending {  get; set; }

        public bool IsPriceDescending { get; set; }

        public bool IsAmountOfBidsAscending { get; set; }

        public bool IsAmountOfBidsDescending { get; set; }

        public string FilterType { get; set; } = string.Empty;
        
        public bool ClosingSoon { get; set; }

        public string? TextSearch { get; set; } = string.Empty;
    }
}
