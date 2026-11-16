using Domain.Entities;
using Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.SeedData
{
    public static class SeedImages
    {

        private static List<AuctionItemImage> images = new List<AuctionItemImage>()
        {
            new AuctionItemImage()
            {                
                ContentType = "image/jpeg",
                Data = SeedDataLoader.LoadImage("woodcut_001.jpeg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/png",
                Data = SeedDataLoader.LoadImage("woodcut_002.png"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpeg",
                Data = SeedDataLoader.LoadImage("woodcut_003.jpeg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/png",
                Data = SeedDataLoader.LoadImage("woodcut_004.png"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpg",
                Data = SeedDataLoader.LoadImage("woodcut_005.jpg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpg",
                Data = SeedDataLoader.LoadImage("woodcut_006.jpg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/png",
                Data = SeedDataLoader.LoadImage("woodcut_007.png"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpeg",
                Data = SeedDataLoader.LoadImage("woodcut_008.jpeg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpeg",
                Data = SeedDataLoader.LoadImage("woodcut_009.jpeg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpeg",
                Data = SeedDataLoader.LoadImage("woodcut_010.jpeg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpeg",
                Data = SeedDataLoader.LoadImage("engraving_001.jpeg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpg",
                Data = SeedDataLoader.LoadImage("engraving_002.jpg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpg",
                Data = SeedDataLoader.LoadImage("engraving_003.jpg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpg",
                Data = SeedDataLoader.LoadImage("engraving_004.jpg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpeg",
                Data = SeedDataLoader.LoadImage("engraving_005.jpeg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpg",
                Data = SeedDataLoader.LoadImage("engraving_006.jpg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpeg",
                Data = SeedDataLoader.LoadImage("engraving_007.jpeg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpeg",
                Data = SeedDataLoader.LoadImage("engraving_008.jpeg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpeg",
                Data = SeedDataLoader.LoadImage("engraving_009.jpeg"),
            },
            new AuctionItemImage()
            {
                ContentType = "image/jpeg",
                Data = SeedDataLoader.LoadImage("engraving_010.jpeg"),
            }
        };

        public static async Task SeedAsync(AppDbContext db)
        {
            int count = await db.AuctionItemImages.Select(x => x.Id).CountAsync();

            if (count == 0)
            {
                // Traverse Artworks and set ArtImage Id's.
                var auctionItems = await db.AuctionItems.ToListAsync();

                if (auctionItems.Count() == images.Count())
                {
                    for (int i = 0; i < auctionItems.Count(); i++)
                    {
                        var image = images[i];
                        auctionItems[i].Image = image;
                        db.AuctionItems.Update(auctionItems[i]);
                        await db.SaveChangesAsync();
                    }
                }
            }
        }

    }
}
