using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SeedData.Configurations
{
    public class AuctionBidConfiguration : IEntityTypeConfiguration<AuctionBid>
    {
        public void Configure(EntityTypeBuilder<AuctionBid> builder)
        {
            builder.HasData(
                new AuctionBid()
                {
                    Id = 1,
                    AuctionItemId = 1,
                    UserId = 1,
                    Amount = 50.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 2,
                    AuctionItemId = 5,
                    UserId = 2,
                    Amount = 53.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 3,
                    AuctionItemId = 5,
                    UserId = 1,
                    Amount = 56.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 4,
                    AuctionItemId = 5,
                    UserId = 1,
                    Amount = 56.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 5,
                    AuctionItemId = 6,
                    UserId = 3,
                    Amount = 53.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 6,
                    AuctionItemId = 6,
                    UserId = 2,
                    Amount = 56.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 7,
                    AuctionItemId = 7,
                    UserId = 2,
                    Amount = 53.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 8,
                    AuctionItemId = 7,
                    UserId = 3,
                    Amount = 56.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 9,
                    AuctionItemId = 7,
                    UserId = 1,
                    Amount = 59.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 10,
                    AuctionItemId = 7,
                    UserId = 3,
                    Amount = 62.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 11,
                    AuctionItemId = 9,
                    UserId = 4,
                    Amount = 53.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 12,
                    AuctionItemId = 9,
                    UserId = 3,
                    Amount = 64.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 13,
                    AuctionItemId = 9,
                    UserId = 2,
                    Amount = 74.50m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 14,
                    AuctionItemId = 9,
                    UserId = 1,
                    Amount = 85.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 15,
                    AuctionItemId = 9,
                    UserId = 4,
                    Amount = 94.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 16,
                    AuctionItemId = 9,
                    UserId = 3,
                    Amount = 104.01m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 17,
                    AuctionItemId = 9,
                    UserId = 2,
                    Amount = 114.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 18,
                    AuctionItemId = 9,
                    UserId = 1,
                    Amount = 123.34m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 19,
                    AuctionItemId = 9,
                    UserId = 4,
                    Amount = 132.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 20,
                    AuctionItemId = 9,
                    UserId = 3,
                    Amount = 141.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 21,
                    AuctionItemId = 9,
                    UserId = 2,
                    Amount = 150.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 22,
                    AuctionItemId = 9,
                    UserId = 1,
                    Amount = 168.85m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 23,
                    AuctionItemId = 9,
                    UserId = 4,
                    Amount = 175.14m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 24,
                    AuctionItemId = 9,
                    UserId = 3,
                    Amount = 185.99m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 25,
                    AuctionItemId = 14,
                    UserId = 2,
                    Amount = 30.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 26,
                    AuctionItemId = 18,
                    UserId = 1,
                    Amount = 30.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 27,
                    AuctionItemId = 18,
                    UserId = 3,
                    Amount = 35.00m,
                    PlacedAt = new DateTime(2026, 8, 6, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 28,
                    AuctionItemId = 22,
                    UserId = 3,
                    Amount = 34.99m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 29,
                    AuctionItemId = 23,
                    UserId = 3,
                    Amount = 39.99m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 30,
                    AuctionItemId = 23,
                    UserId = 1,
                    Amount = 54.00m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 31,
                    AuctionItemId = 23,
                    UserId = 2,
                    Amount = 64.99m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 32,
                    AuctionItemId = 28,
                    UserId = 4,
                    Amount = 29.99m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 33,
                    AuctionItemId = 28,
                    UserId = 4,
                    Amount = 44.99m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 34,
                    AuctionItemId = 28,
                    UserId = 1,
                    Amount = 51.99m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 35,
                    AuctionItemId = 28,
                    UserId = 2,
                    Amount = 59.99m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 36,
                    AuctionItemId = 28,
                    UserId = 3,
                    Amount = 68.45m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 37,
                    AuctionItemId = 28,
                    UserId = 4,
                    Amount = 77.25m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 38,
                    AuctionItemId = 28,
                    UserId = 1,
                    Amount = 89.65m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 39,
                    AuctionItemId = 29,
                    UserId = 4,
                    Amount = 75.25m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 40,
                    AuctionItemId = 30,
                    UserId = 1,
                    Amount = 75.99m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 41,
                    AuctionItemId = 30,
                    UserId = 3,
                    Amount = 110.00m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                },
                new AuctionBid()
                {
                    Id = 42,
                    AuctionItemId = 30,
                    UserId = 2,
                    Amount = 135.85m,
                    PlacedAt = new DateTime(2026, 11, 17, 0, 0, 0, DateTimeKind.Utc),
                }
            );
        }
    }
}
