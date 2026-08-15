using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;

namespace Domain.Entities
{

    [Table("AuctionItems", Schema = "Auction")]
    public class AuctionItem
    {
        [Key]
        public int Id { get; set; }

        public AuctionStatus Status { get; set; }

        [ForeignKey("Detail")]
        public int AuctionItemDetailId { get; set; }
        public AuctionItemDetail Detail { get; set; }

        [ForeignKey("Image")]
        public int AuctionItemImageId { get; set; }
        public AuctionItemImage Image { get; set; }

        // Denormalized cache columns (fast reads, no aggregation)  
        public decimal CurrentPrice { get; set; }
        public int BidCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTimeOffset EndsAt { get; set; }

        public int AuctionLength { get; set; }

        // optimistic concurrency  
        public uint Version { get; set; }

        public ICollection<AuctionBid> Bids { get; set; }
    }
}
