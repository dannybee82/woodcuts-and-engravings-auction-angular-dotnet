using Application.DataTransferObjects;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace AuctionWoodcutsEngravingsWebApi.Controllers
{

    [ApiController]
    [Route("helpers")]
    public class HelpersController : ControllerBase
    {
        private readonly IHelpersService _helpersService;

        public HelpersController(IHelpersService helpersService)
        {
            _helpersService = helpersService;
        }


        [HttpGet("SimulateBidForUser")]
        public async Task<IActionResult> SimulateBidForUser([FromQuery] HelperSimulateBidDto dto)
        {
            try
            {
                var message = await _helpersService.SimulateBid(dto);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("EndAuctionSoon")]
        public async Task<IActionResult> EndAuctionSoon(int auctionItemId)
        {
            try
            {
                var message = await _helpersService.EndAuctionSoon(auctionItemId);
                return Ok(message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}
