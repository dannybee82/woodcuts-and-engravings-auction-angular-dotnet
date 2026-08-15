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
                  AuctionItemImageId = 1,
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
                  AuctionItemImageId = 2,
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
                  AuctionItemImageId = 3,
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
                  AuctionItemImageId = 4,
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
                  AuctionItemImageId = 5,
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
                  AuctionItemImageId = 6,
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
                  AuctionItemImageId = 7,
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
                  AuctionItemImageId = 8,
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
                  AuctionItemImageId = 9,
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
                  AuctionItemImageId = 10,
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
                  AuctionItemImageId = 11,
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
                  AuctionItemImageId = 12,
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
                  AuctionItemImageId = 13,
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
                  AuctionItemImageId = 14,
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
                  AuctionItemImageId = 15,
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
                  AuctionItemImageId = 16,
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
                  AuctionItemImageId = 17,
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
                  AuctionItemImageId = 18,
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
                  AuctionItemImageId = 19,
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
                  AuctionItemImageId = 20,
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
