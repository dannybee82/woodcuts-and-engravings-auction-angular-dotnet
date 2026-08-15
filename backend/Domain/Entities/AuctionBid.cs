using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    [Table("AuctionBids", Schema = "Auction")]
    public class AuctionBid
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        public DummyUser User { get; set; }

        public decimal Amount { get; set; }

        public DateTimeOffset PlacedAt { get; set; }

        [ForeignKey("AuctionItem")]
        public int AuctionItemId { get; set; }

        public AuctionItem? AuctionItem { get; set; }

    }
}
