using Application.Mappers;
using AuctionWoodcutsEngravingsWebApi.hubs;
using Domain.Enums;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.BackgroundJobs
{
    public class AuctionCloser
    {
        private readonly IHubContext<AuctionHub> _hub;
        private readonly AppDbContext _db;
        private readonly IAuctionWonItemRepository _auctionSoldItemRepository;

        public AuctionCloser(IHubContext<AuctionHub> hub, AppDbContext db, IAuctionWonItemRepository auctionSoldItemRepository)
        {
            _hub = hub;
            _db = db;
            _auctionSoldItemRepository = auctionSoldItemRepository;
        }

        public async Task CloseAuction(int itemId)
        {
            var item = await _db.AuctionItems.SingleOrDefaultAsync(x => x.Id == itemId);
            if (item is null || item.Status == AuctionStatus.CLOSED) return;

            item.Status = AuctionStatus.CLOSED;
            await _db.SaveChangesAsync();

            await _hub.Clients
                .Group($"item-{itemId}")
                .SendAsync("AuctionEnded", new { itemId, winningBid = item.CurrentPrice });

            if(item.BidCount > 0)
            {
                await SaveSoldItem(itemId);
            }
        }

        public async Task SaveSoldItem(int itemId)
        {
            var item = await _db.AuctionItems.AsNoTracking()
                .Include(x => x.Bids.OrderByDescending(x => x.Amount).ThenBy(x => x.PlacedAt).ThenBy(x => x.Id).Take(1))
                .SingleOrDefaultAsync(x => x.Id == itemId);

            if(item == null) return;

            var auctionWonByUser = item.Bids.First();

            if (auctionWonByUser != null)
            {
                var entity = AuctionSoldItemMapper.ToEntity(itemId, item.CurrentPrice, auctionWonByUser.UserId);
                await _auctionSoldItemRepository.Create(entity);
            }
        }

    }
}
