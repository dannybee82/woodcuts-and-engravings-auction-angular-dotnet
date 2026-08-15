using Application.DataTransferObjects;
using Domain.Entities;
using Domain.Paging;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public interface IAuctionItemService
    {
        Task<PaginatedListDto<AuctionItemDto>> GetPagedList(int pageNumber, int pageSize, AuctionItemFilterDto? filters);

        Task<List<AuctionItemDto>> GetAll();

        Task<AuctionItemDto> GetById(int id);

        Task CreateAuction(IFormFile file, CreateOrUpdateAuctionDto dto);

        Task UpdateAuction(IFormFile? file, CreateOrUpdateAuctionDto dto);

        Task DeleteAuction(int id);

        Task<AuctionItemImage?> GetImage(int id);

    }
}
