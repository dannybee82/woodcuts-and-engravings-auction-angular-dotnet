using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SeedData.Configurations
{
    internal class AuctionItemImageConfiguration : IEntityTypeConfiguration<AuctionItemImage>
    {
        public void Configure(EntityTypeBuilder<AuctionItemImage> builder)
        {
            builder.HasData(
                new AuctionItemImage()
                {
                    Id = 1,
                    ContentType = "image/jpeg",
                    Data = SeedDataLoader.LoadImage("woodcut_001.jpeg"),
                },
                new AuctionItemImage()
                {
                    Id = 2,
                    ContentType = "image/png",
                    Data = SeedDataLoader.LoadImage("woodcut_002.png"),
                },
                new AuctionItemImage()
                {
                    Id = 3,
                    ContentType = "image/jpeg",
                    Data = SeedDataLoader.LoadImage("woodcut_003.jpeg"),
                },
                new AuctionItemImage()
                {
                    Id = 4,
                    ContentType = "image/png",
                    Data = SeedDataLoader.LoadImage("woodcut_004.png"),
                },
                new AuctionItemImage()
                {
                    Id = 5,
                    ContentType = "image/jpg",
                    Data = SeedDataLoader.LoadImage("woodcut_005.jpg"),
                },
                new AuctionItemImage()
                {
                    Id = 6,
                    ContentType = "image/jpg",
                    Data = SeedDataLoader.LoadImage("woodcut_006.jpg"),
                },
                new AuctionItemImage()
                {
                    Id = 7,
                    ContentType = "image/png",
                    Data = SeedDataLoader.LoadImage("woodcut_007.png"),
                },
                new AuctionItemImage()
                {
                    Id = 8,
                    ContentType = "image/jpeg",
                    Data = SeedDataLoader.LoadImage("woodcut_008.jpeg"),
                },
                new AuctionItemImage()
                {
                    Id = 9,
                    ContentType = "image/jpeg",
                    Data = SeedDataLoader.LoadImage("woodcut_009.jpeg"),
                },
                new AuctionItemImage()
                {
                    Id = 10,
                    ContentType = "image/jpeg",
                    Data = SeedDataLoader.LoadImage("woodcut_010.jpeg"),
                },
                new AuctionItemImage()
                {
                    Id = 11,
                    ContentType = "image/jpeg",
                    Data = SeedDataLoader.LoadImage("engraving_001.jpeg"),
                },
                new AuctionItemImage()
                {
                    Id = 12,
                    ContentType = "image/jpg",
                    Data = SeedDataLoader.LoadImage("engraving_002.jpg"),
                },
                new AuctionItemImage()
                {
                    Id = 13,
                    ContentType = "image/jpg",
                    Data = SeedDataLoader.LoadImage("engraving_003.jpg"),
                },
                new AuctionItemImage()
                {
                    Id = 14,
                    ContentType = "image/jpg",
                    Data = SeedDataLoader.LoadImage("engraving_004.jpg"),
                },
                new AuctionItemImage()
                {
                    Id = 15,
                    ContentType = "image/jpeg",
                    Data = SeedDataLoader.LoadImage("engraving_005.jpeg"),
                },
                new AuctionItemImage()
                {
                    Id = 16,
                    ContentType = "image/jpg",
                    Data = SeedDataLoader.LoadImage("engraving_006.jpg"),
                },
                new AuctionItemImage()
                {
                    Id = 17,
                    ContentType = "image/jpeg",
                    Data = SeedDataLoader.LoadImage("engraving_007.jpeg"),
                },
                new AuctionItemImage()
                {
                    Id = 18,
                    ContentType = "image/jpeg",
                    Data = SeedDataLoader.LoadImage("engraving_008.jpeg"),
                },
                new AuctionItemImage()
                {
                    Id = 19,
                    ContentType = "image/jpeg",
                    Data = SeedDataLoader.LoadImage("engraving_009.jpeg"),
                },
                new AuctionItemImage()
                {
                    Id = 20,
                    ContentType = "image/jpeg",
                    Data = SeedDataLoader.LoadImage("engraving_010.jpeg"),
                }
            );
        }
    }
}
