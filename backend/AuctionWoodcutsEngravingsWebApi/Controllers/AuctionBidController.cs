using Application.DataTransferObjects;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionWoodcutsEngravingsWebApi.Controllers
{

    [ApiController]
    [Route("bids")]
    public class AuctionBidController : ControllerBase
    {
        private readonly IAuctionBidService _auctionBidService;

        public AuctionBidController(IAuctionBidService auctionBidService)
        {
            _auctionBidService = auctionBidService;
        }

        [HttpPost("PlaceBid")]
        public async Task<IActionResult> PlaceBid([FromBody] PlaceBidDto dto)
        {
            try
            {
                bool bidPlaced = await _auctionBidService.PlaceBid(dto.AuctionItemId, dto.UserId, dto.Amount);

                if(bidPlaced)
                {
                    return Ok();
                }
                else
                {
                    return BadRequest();
                }                
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpGet("GetLatestBids")]
        public async Task<IActionResult> GetLatestBids(int id)
        {
            try
            {
                var items = await _auctionBidService.GetLatestBids(id);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
