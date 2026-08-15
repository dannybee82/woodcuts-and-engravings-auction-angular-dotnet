using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{

    [Table("AuctionWonItems", Schema = "Auction")]
    public class AuctionWonItem
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("Item")]
        public int AuctionItemId { get; set; }

        public AuctionItem Item { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public DummyUser User { get; set; }

        public decimal Price { get; set; }
    }
}
