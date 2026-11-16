using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Auction");

            migrationBuilder.CreateTable(
                name: "AuctionItemDetails",
                schema: "Auction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    ItemType = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItemDetails", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuctionItemImages",
                schema: "Auction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ContentType = table.Column<string>(type: "text", nullable: false),
                    Data = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItemImages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DummyUsers",
                schema: "Auction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DummyUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuctionItems",
                schema: "Auction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    AuctionItemDetailId = table.Column<int>(type: "integer", nullable: false),
                    AuctionItemImageId = table.Column<int>(type: "integer", nullable: true),
                    CurrentPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    BidCount = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    EndsAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AuctionLength = table.Column<int>(type: "integer", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionItems_AuctionItemDetails_AuctionItemDetailId",
                        column: x => x.AuctionItemDetailId,
                        principalSchema: "Auction",
                        principalTable: "AuctionItemDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionItems_AuctionItemImages_AuctionItemImageId",
                        column: x => x.AuctionItemImageId,
                        principalSchema: "Auction",
                        principalTable: "AuctionItemImages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AuctionBids",
                schema: "Auction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    PlacedAt = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    AuctionItemId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionBids", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionBids_AuctionItems_AuctionItemId",
                        column: x => x.AuctionItemId,
                        principalSchema: "Auction",
                        principalTable: "AuctionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionBids_DummyUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auction",
                        principalTable: "DummyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AuctionWonItems",
                schema: "Auction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AuctionItemId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionWonItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AuctionWonItems_AuctionItems_AuctionItemId",
                        column: x => x.AuctionItemId,
                        principalSchema: "Auction",
                        principalTable: "AuctionItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuctionWonItems_DummyUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Auction",
                        principalTable: "DummyUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "Auction",
                table: "AuctionItemDetails",
                columns: new[] { "Id", "Description", "ItemType", "Title" },
                values: new object[,]
                {
                    { 1, "This woodcut is a tribute to the gentle and kind nun Anna the Benign.", 1, "A Hommage to Anna the Benign" },
                    { 2, "This woodcut depicts a Native American Indian woman in traditional clothing.", 1, "Native American Woman" },
                    { 3, "The woodcut depicts an unknown Victorian woman in a turquoise blouse with rich brocade in floral motifs.", 1, "Victorian Lady in Turquoise Blouse" },
                    { 4, "This woodcut has an exotic theme: it shows a priestess holding a gemstone, and next to her sits a manticore.", 1, "The Eye of the Manticore" },
                    { 5, "The woodcut depicts Olivia des Beauchamps's visit to the museum, with a presumably Phoenician goddess of the moon and stars on the left.", 1, "Olivia des Beauchamps visiting the Museum" },
                    { 6, "The woodcut shows Olivia des Beauchamps on the swing, and she is wearing an 18th-century dress.", 1, "Olivia des Beauchamps on the swing" },
                    { 7, "A woodcut with an artistic theme: on the left is an ancient Greek statue of a muse, in the middle is Olivia des Beauchamps, and on the right is an ancient Greek vase. Who is the real muse?", 1, "Olivia des Beauchamps and the True Muse" },
                    { 8, "A detailed woodcut showing a woman with dark hair wearing a matching black velvet dress.", 1, "The Velvet Diva" },
                    { 9, "A woodcut with a deeply religious theme. The woodcut depicts a woman sitting on the ground and in a deep meditation.", 1, "Another Deep Meditation" },
                    { 10, "A very detailed woodcut depicting an angel from the celestial realm.", 1, "The Angelic Realm" },
                    { 11, "A hand-colored engraving showing a beautiful Gypsy girl with dark hair in richly colored clothing.", 2, "The beautiful Gypsy Girl" },
                    { 12, "A colored and detailed engraving depicting a wealthy Victorian woman drinking tea during lunch break.", 2, "Victorian Woman during the Lunch Break" },
                    { 13, "A colored and detailed engraving showing an angel with a sword before the Day of Judgment.", 2, "Angel of Judgment with Sword" },
                    { 14, "A hand-colored and highly detailed engraving showing Olivia des Beauchamps in a stunning 18th-century dress for the evening Soiree.", 2, "Olivia des Beauchamps is all dressed up for the Soiree" },
                    { 15, "A very detailed engraving showing a woman in a black dress in the foreground and a spiderweb in the background. This engraving is a reference to the venomous spider (the black widow).", 2, "The real Black Widow" },
                    { 16, "A colored engraving depicting the noblewoman Éléonore de Valcour.", 2, "Engraving of Éléonore de Valcour" },
                    { 17, "A playful engraving titled 'Elysia - Muse of Wishes'. Unfortunately, the engraving is slightly damaged.", 2, "Elysia - Muse of Wishes" },
                    { 18, "This engraving shows the expert jewelry thief 'Marcella Dupond' at work in her field: stealing jewels and jewelry.", 2, "Marcella Dupond - Expert Jewel Thief" },
                    { 19, "Detailed and colored engraving depicting an unknown Lady in 18th-century attire.", 2, "18th century Lady standing near the Window" },
                    { 20, "This colored engraving depicts a 19th-century Lady wearing a very fine (and precious) freshwater pearl necklace.", 2, "Victorian Lady with exquisite pearl necklace" }
                });

            migrationBuilder.InsertData(
                schema: "Auction",
                table: "DummyUsers",
                columns: new[] { "Id", "UserName" },
                values: new object[,]
                {
                    { 1, "Norbert Nikolaus" },
                    { 2, "Jean Millet" },
                    { 3, "August Auriel" },
                    { 4, "Lucien Delacroix" },
                    { 5, "Logged In User" }
                });

            migrationBuilder.InsertData(
                schema: "Auction",
                table: "AuctionItems",
                columns: new[] { "Id", "AuctionItemDetailId", "AuctionItemImageId", "AuctionLength", "BidCount", "CreatedAt", "CurrentPrice", "EndsAt", "Status" },
                values: new object[,]
                {
                    { 1, 1, null, 14, 1, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 50.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 2, 2, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 25.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 3, 3, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 25.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 4, 4, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 75.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 5, 5, null, 14, 3, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 59.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 6, 6, null, 14, 2, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 56.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 7, 7, null, 14, 4, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 62.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 8, 8, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 40.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 9, 9, null, 14, 14, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 185.99m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 10, 10, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 50.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 11, 11, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 25.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 12, 12, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 25.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 13, 13, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 25.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 14, 14, null, 14, 1, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 30.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 15, 15, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 25.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 16, 16, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 25.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 17, 17, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 25.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 18, 18, null, 14, 2, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 35.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 19, 19, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 25.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 20, 20, null, 14, 0, new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Utc), 25.00m, new DateTimeOffset(new DateTime(2026, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 }
                });

            migrationBuilder.InsertData(
                schema: "Auction",
                table: "AuctionBids",
                columns: new[] { "Id", "Amount", "AuctionItemId", "PlacedAt", "UserId" },
                values: new object[,]
                {
                    { 1, 50.00m, 1, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 2, 53.00m, 5, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 3, 56.00m, 5, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 4, 56.00m, 5, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 5, 53.00m, 6, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 6, 56.00m, 6, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 7, 53.00m, 7, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 8, 56.00m, 7, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 9, 59.00m, 7, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 10, 62.00m, 7, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 11, 53.00m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 12, 64.00m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 13, 74.50m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 14, 85.00m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 15, 94.00m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 16, 104.01m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 17, 114.00m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 18, 123.34m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 19, 132.00m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 20, 141.00m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 21, 150.00m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 22, 168.85m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 23, 175.14m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 4 },
                    { 24, 185.99m, 9, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 },
                    { 25, 30.00m, 14, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 2 },
                    { 26, 30.00m, 18, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 1 },
                    { 27, 35.00m, 18, new DateTimeOffset(new DateTime(2026, 8, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)), 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuctionBids_AuctionItemId",
                schema: "Auction",
                table: "AuctionBids",
                column: "AuctionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionBids_UserId",
                schema: "Auction",
                table: "AuctionBids",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItems_AuctionItemDetailId",
                schema: "Auction",
                table: "AuctionItems",
                column: "AuctionItemDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionItems_AuctionItemImageId",
                schema: "Auction",
                table: "AuctionItems",
                column: "AuctionItemImageId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionWonItems_AuctionItemId",
                schema: "Auction",
                table: "AuctionWonItems",
                column: "AuctionItemId");

            migrationBuilder.CreateIndex(
                name: "IX_AuctionWonItems_UserId",
                schema: "Auction",
                table: "AuctionWonItems",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuctionBids",
                schema: "Auction");

            migrationBuilder.DropTable(
                name: "AuctionWonItems",
                schema: "Auction");

            migrationBuilder.DropTable(
                name: "AuctionItems",
                schema: "Auction");

            migrationBuilder.DropTable(
                name: "DummyUsers",
                schema: "Auction");

            migrationBuilder.DropTable(
                name: "AuctionItemDetails",
                schema: "Auction");

            migrationBuilder.DropTable(
                name: "AuctionItemImages",
                schema: "Auction");
        }
    }
}
