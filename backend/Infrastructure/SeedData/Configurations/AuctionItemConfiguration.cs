using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Reflection.Emit;
using System.Text;

namespace Infrastructure.SeedData.Configurations
{
    public class AuctionItemConfiguration : IEntityTypeConfiguration<AuctionItem>
    {
        public void Configure(EntityTypeBuilder<AuctionItem> builder)
        {
            builder.HasData(
              new AuctionItem()
              {
                  Id = 1,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 1,
                  AuctionItemImageId = null,
                  CurrentPrice = 50.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 1,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 2,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 2,
                  AuctionItemImageId = null,
                  CurrentPrice = 25.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 3,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 3,
                  AuctionItemImageId = null,
                  CurrentPrice = 25.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 4,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 4,
                  AuctionItemImageId = null,
                  CurrentPrice = 75.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 5,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 5,
                  AuctionItemImageId = null,
                  CurrentPrice = 59.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 3,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 6,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 6,
                  AuctionItemImageId = null,
                  CurrentPrice = 56.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 2,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 7,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 7,
                  AuctionItemImageId = null,
                  CurrentPrice = 62.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 4,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 8,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 8,
                  AuctionItemImageId = null,
                  CurrentPrice = 40.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 9,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 9,
                  AuctionItemImageId = null,
                  CurrentPrice = 185.99m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 14,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 10,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 10,
                  AuctionItemImageId = null,
                  CurrentPrice = 50.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 11,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 11,
                  AuctionItemImageId = null,
                  CurrentPrice = 25.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 12,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 12,
                  AuctionItemImageId = null,
                  CurrentPrice = 25.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 13,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 13,
                  AuctionItemImageId = null,
                  CurrentPrice = 25.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 14,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 14,
                  AuctionItemImageId = null,
                  CurrentPrice = 30.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 1,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 15,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 15,
                  AuctionItemImageId = null,
                  CurrentPrice = 25.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 16,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 16,
                  AuctionItemImageId = null,
                  CurrentPrice = 25.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 17,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 17,
                  AuctionItemImageId = null,
                  CurrentPrice = 25.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 18,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 18,
                  AuctionItemImageId = null,
                  CurrentPrice = 35.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 2,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 19,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 19,
                  AuctionItemImageId = null,
                  CurrentPrice = 25.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              },
              new AuctionItem()
              {
                  Id = 20,
                  Status = AuctionStatus.OPEN,
                  AuctionItemDetailId = 20,
                  AuctionItemImageId = null,
                  CurrentPrice = 25.00m,
                  CreatedAt = new DateTime(2026, 8, 13, 0, 0, 0, DateTimeKind.Utc),
                  BidCount = 0,
                  EndsAt = new DateTime(2026, 8, 27, 0, 0, 0, DateTimeKind.Utc),
                  AuctionLength = 14
              }
            );
        }
    }
}
