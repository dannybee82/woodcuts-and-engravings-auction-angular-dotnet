using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.SeedData.Configurations
{
    public class AuctionItemDetailConfiguration : IEntityTypeConfiguration<AuctionItemDetail>
    {
        public void Configure(EntityTypeBuilder<AuctionItemDetail> builder)
        {
            builder.HasData(
                new AuctionItemDetail()
                {
                    Id = 1,
                    Title = "A Hommage to Anna the Benign",
                    Description = "This woodcut is a tribute to the gentle and kind nun Anna the Benign.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 2,
                    Title = "Native American Woman",
                    Description = "This woodcut depicts a Native American Indian woman in traditional clothing.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 3,
                    Title = "Victorian Lady in Turquoise Blouse",
                    Description = "The woodcut depicts an unknown Victorian woman in a turquoise blouse with rich brocade in floral motifs.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 4,
                    Title = "The Eye of the Manticore",
                    Description = "This woodcut has an exotic theme: it shows a priestess holding a gemstone, and next to her sits a manticore.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 5,
                    Title = "Olivia des Beauchamps visiting the Museum",
                    Description = "The woodcut depicts Olivia des Beauchamps's visit to the museum, with a presumably Phoenician goddess of the moon and stars on the left.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 6,
                    Title = "Olivia des Beauchamps on the swing",
                    Description = "The woodcut shows Olivia des Beauchamps on the swing, and she is wearing an 18th-century dress.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 7,
                    Title = "Olivia des Beauchamps and the True Muse",
                    Description = "A woodcut with an artistic theme: on the left is an ancient Greek statue of a muse, in the middle is Olivia des Beauchamps, and on the right is an ancient Greek vase. Who is the real muse?",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 8,
                    Title = "The Velvet Diva",
                    Description = "A detailed woodcut showing a woman with dark hair wearing a matching black velvet dress.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 9,
                    Title = "Another Deep Meditation",
                    Description = "A woodcut with a deeply religious theme. The woodcut depicts a woman sitting on the ground and in a deep meditation.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 10,
                    Title = "The Angelic Realm",
                    Description = "A very detailed woodcut depicting an angel from the celestial realm.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 11,
                    Title = "The beautiful Gypsy Girl",
                    Description = "A hand-colored engraving showing a beautiful Gypsy girl with dark hair in richly colored clothing.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 12,
                    Title = "Victorian Woman during the Lunch Break",
                    Description = "A colored and detailed engraving depicting a wealthy Victorian woman drinking tea during lunch break.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 13,
                    Title = "Angel of Judgment with Sword",
                    Description = "A colored and detailed engraving showing an angel with a sword before the Day of Judgment.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 14,
                    Title = "Olivia des Beauchamps is all dressed up for the Soiree",
                    Description = "A hand-colored and highly detailed engraving showing Olivia des Beauchamps in a stunning 18th-century dress for the evening Soiree.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 15,
                    Title = "The real Black Widow",
                    Description = "A very detailed engraving showing a woman in a black dress in the foreground and a spiderweb in the background. This engraving is a reference to the venomous spider (the black widow).",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 16,
                    Title = "Engraving of Éléonore de Valcour",
                    Description = "A colored engraving depicting the noblewoman Éléonore de Valcour.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 17,
                    Title = "Elysia - Muse of Wishes",
                    Description = "A playful engraving titled 'Elysia - Muse of Wishes'. Unfortunately, the engraving is slightly damaged.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 18,
                    Title = "Marcella Dupond - Expert Jewel Thief",
                    Description = "This engraving shows the expert jewelry thief 'Marcella Dupond' at work in her field: stealing jewels and jewelry.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 19,
                    Title = "18th century Lady standing near the Window",
                    Description = "Detailed and colored engraving depicting an unknown Lady in 18th-century attire.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 20,
                    Title = "Victorian Lady with exquisite pearl necklace",
                    Description = "This colored engraving depicts a 19th-century Lady wearing a very fine (and precious) freshwater pearl necklace.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 21,
                    Title = "Juniper Hale in the Secret Garden",
                    Description = "A woodcut depicting an exceptionally beautiful woman - with the name Juniper Hale - surrounded by floral motifs.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 22,
                    Title = "The Alabaster Jar",
                    Description = "This engraving alludes to Mary Magdalene, who is identifiable by the jar of ointment.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 23,
                    Title = "The Lady of the Lake",
                    Description = "The woodcut depicts a woman walking from the water toward the viewer under the full moon.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 24,
                    Title = "Ebony and Gold",
                    Description = "This engraving depicts an exotic woman from a distant, sunny land, dressed in traditional black attire adorned with gold-colored brocade.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 25,
                    Title = "Song of the Ancient Grove",
                    Description = "The woodcut depicts an indigenous woman situated in a very old forest.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 26,
                    Title = "The Celestial Edict",
                    Description = "In this engraving, an angel of judgment can be seen holding a sword, with light emanating from the background.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 27,
                    Title = "Silence and Columns: A Muse Contemplative",
                    Description = "The woodcut depicts a muse from antiquity standing with her back against a wall, with classical columns in the background.",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 28,
                    Title = "Vigil at Midnight",
                    Description = "A mysterious engraving depicting a woman in nightgown, holding a candle in her hands.",
                    ItemType = ItemType.ENGRAVING,
                },
                new AuctionItemDetail()
                {
                    Id = 29,
                    Title = "The Revised Spirit of the Summit",
                    Description = "This woodcut is a reinterpretation of the painting by Frederic Leighton titled \"The Spirit of the Summit\".",
                    ItemType = ItemType.WOODCUT,
                },
                new AuctionItemDetail()
                {
                    Id = 30,
                    Title = "The Knight and his Apparition",
                    Description = "This engraving is highly symbolic: a knight kneels before a statue, with lightning and a temple visible in the background, while an apparition of a woman appears within the dark clouds.",
                    ItemType = ItemType.ENGRAVING,
                }
            );
        }
    }
}
