using Application.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace AuctionWoodcutsEngravingsWebApi.Controllers
{

    [ApiController]
    [Route("auction-won")]
    public class AuctionWonController : ControllerBase
    {
        private readonly IAuctionWonItemService _auctionWonItemService; 

        public AuctionWonController(IAuctionWonItemService auctionWonItemService)
        {
            _auctionWonItemService = auctionWonItemService;
        }


        [HttpGet("GetAllWonItems")]
        public async Task<IActionResult> GetAllWonItems(int id)
        {
            try
            {
                var items = await _auctionWonItemService.GetAllWonItems(id);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
