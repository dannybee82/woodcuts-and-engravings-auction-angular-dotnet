using Application.BackgroundJobs;
using Application.DataTransferObjects;
using AuctionWoodcutsEngravingsWebApi.hubs;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class HelpersService : IHelpersService
    {
        private readonly IHubContext<AuctionHub> _hub;
        private readonly IAuctionBidTransaction _auctionBidTransaction;
        private readonly IDummyUserRepository _dummyUserRepository;
        private readonly IAuctionItemRepository _auctionItemRepository;
        private readonly IServiceProvider _services;

        public HelpersService(
            IHubContext<AuctionHub> hub,
            IAuctionBidTransaction auctionBidTransaction,
            IDummyUserRepository dummyUserRepository,
            IAuctionItemRepository auctionItemRepository,
            IServiceProvider services
        )
        {
            _hub = hub;
            _auctionBidTransaction = auctionBidTransaction;
            _dummyUserRepository = dummyUserRepository;
            _auctionItemRepository = auctionItemRepository;
            _services = services;
        }

        public async Task<string> EndAuctionSoon(int auctionItemId)
        {
            var item = await _auctionItemRepository.GetById(auctionItemId);

            if(item == null)
            {
                throw new Exception("Item doesn't exist.");
            }

            try
            {
                var now = DateTimeOffset.UtcNow;
                var soon = now.AddMinutes(3);
                item.EndsAt = soon;

                await _auctionItemRepository.Update(item);

                // Rescheldule.
                using var scope = _services.CreateScope();
                var scheduler = scope.ServiceProvider.GetRequiredService<IBackgroundJobScheduler>();
                scheduler.Schedule<AuctionCloser>(x => x.CloseAuction(item.Id), item.EndsAt);

                return "Item ending in 3 minutes. Please refresh the frontend page.";
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<string> SimulateBid(HelperSimulateBidDto dto)
        {
            if(!await _dummyUserRepository.UserExists(dto.UserId))
            {
                throw new Exception("User doesn't exist.");
            }

            var item =  await _auctionBidTransaction.PlaceBid(dto.AuctionItemId, dto.UserId, dto.Amount);

            if (item != null)
            {
                await _hub.Clients
                .Group($"item-{dto.AuctionItemId}")
                .SendAsync("BidPlaced", new { dto.AuctionItemId, currentPrice = item.CurrentPrice, bidCount = item.BidCount });
                return "Bid has been placed successfully.";
            }
            else
            {
                throw new Exception("Can't place bid: Auction Item doesn't exist or amount too low.");
            }
        }
    }
}
