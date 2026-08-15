using Application.DataTransferObjects;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public class AuctionBidDetailMapper
    {

        public static List<AuctionBidDetailDto> ToDtoList(List<AuctionBid> entitites)
        {
            if(entitites.Count() == 0)
            {
                return new List<AuctionBidDetailDto>();
            }

            return entitites.Select(x => ToDto(x)).ToList();
        }


        private static AuctionBidDetailDto ToDto(AuctionBid entity)
        {
            return new AuctionBidDetailDto()
            {
                Amount = entity.Amount,
                UserName = entity.User.UserName
            };
        }

    }
}
