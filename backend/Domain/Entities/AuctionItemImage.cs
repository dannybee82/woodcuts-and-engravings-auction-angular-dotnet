using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    [Table("AuctionItemImages", Schema = "Auction")]
    public class AuctionItemImage
    {

        [Key]
        public int Id { get; set; }

        public string ContentType { get; set; }

        public byte[] Data { get; set; }
    }
}
