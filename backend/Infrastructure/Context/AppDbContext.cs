using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Npgsql;

namespace Infrastructure.Context
{
    public class AppDbContext : DbContext
    {

        public DbSet<AuctionItem> AuctionItems { get; set; }
        public DbSet<AuctionItemDetail> AuctionItemDetails { get; set; }
        public DbSet<AuctionItemImage> AuctionItemImages { get; set; }
        public DbSet<AuctionBid> AuctionBids { get; set; }
        public DbSet<AuctionWonItem> AuctionWonItems { get; set; }
        public DbSet<DummyUser> Users { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AuctionItem>().Property(b => b.Version).IsRowVersion();

            modelBuilder.Entity<AuctionItem>()
                .HasMany(x => x.Bids)
                .WithOne(x => x.AuctionItem)
                .HasForeignKey(ssf => ssf.AuctionItemId)
                .OnDelete(DeleteBehavior.Cascade);

            // Applies ImageConfiguration (and any others) automatically  
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

    }
}
