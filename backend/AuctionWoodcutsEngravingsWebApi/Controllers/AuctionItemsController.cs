using Application.DataTransferObjects;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace AuctionWoodcutsEngravingsWebApi.Controllers
{
    [ApiController]
    [Route("auction-items")]
    public class AuctionItemsController : ControllerBase
    {
        private readonly IAuctionItemService _auctionItemService;

        public AuctionItemsController(IAuctionItemService auctionItemService)
        {
            _auctionItemService = auctionItemService;
        }

        [HttpGet("GetPagedList")]
        public async Task<IActionResult> GetPagedList(int pageNumber, int pageSize, [FromQuery] AuctionItemFilterDto? filters)
        {
            try
            {
                var pagedList = await _auctionItemService.GetPagedList(pageNumber, pageSize, filters);
                return Ok(pagedList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetImage(int id)
        {
            try
            {
                var img = await _auctionItemService.GetImage(id);
                if (img is null) return NotFound();
                // streams raw bytes
                return File(img.Data, img.ContentType);   
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var items = await _auctionItemService.GetAll();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var item = await _auctionItemService.GetById(id);
                return Ok(item);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Create")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create(IFormFile file, [FromForm] CreateOrUpdateAuctionDto dto)
        {
            try
            {
                await _auctionItemService.CreateAuction(file, dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(IFormFile? file, [FromForm] CreateOrUpdateAuctionDto dto)
        {
            try
            {
                await _auctionItemService.UpdateAuction(file, dto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _auctionItemService.DeleteAuction(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }

}
