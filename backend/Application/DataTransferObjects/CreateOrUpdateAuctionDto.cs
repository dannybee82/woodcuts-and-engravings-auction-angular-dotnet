using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.DataTransferObjects
{
    public class CreateOrUpdateAuctionDto
    {

        public int? Id {  get; set; }

        public bool IsOpen { get; set; }

        public AuctionItemDetailDto Details { get; set; }

        public decimal CurrentPrice { get; set; }

        public int AuctionLength { get; set; }
    }
}