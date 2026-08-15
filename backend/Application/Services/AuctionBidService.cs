using Application.DataTransferObjects;
using Application.Mappers;
using AuctionWoodcutsEngravingsWebApi.hubs;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class AuctionBidService : IAuctionBidService
    {
        private readonly IHubContext<AuctionHub> _hub;
        private readonly IAuctionBidTransaction _auctionBidTransaction;
        private readonly IAuctionBidRepository _auctionBidRepository;

        public AuctionBidService(
            IHubContext<AuctionHub> hub, 
            IAuctionBidTransaction auctionBidTransaction,
            IAuctionBidRepository auctionBidRepository)
        {
            _hub = hub;
            _auctionBidTransaction = auctionBidTransaction;
            _auctionBidRepository = auctionBidRepository;
        }

        public async Task<bool> PlaceBid(int auctionItemId, int userId, decimal amount)
        {
            try
            {
                var item = await _auctionBidTransaction.PlaceBid(auctionItemId, userId, amount);

                if(item != null)
                {
                    await _hub.Clients
                    .Group($"item-{auctionItemId}")
                    .SendAsync("BidPlaced", new { auctionItemId, currentPrice = item.CurrentPrice, bidCount = item.BidCount });
                    return true;
                }
                else
                {
                    return false;
                }                
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<List<AuctionBidDetailDto>> GetLatestBids(int auctionId)
        {
            var records = await _auctionBidRepository.GetLatestBids(auctionId);

            return records.Count() == 0 ?
                new List<AuctionBidDetailDto>() :
                AuctionBidDetailMapper.ToDtoList(records);
        }
    }
}
