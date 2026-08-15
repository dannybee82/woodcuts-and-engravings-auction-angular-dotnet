using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace AuctionWoodcutsEngravingsWebApi.hubs
{
    public class AuctionHub : Hub
    {
        public Task JoinItem(int itemId) =>
            Groups.AddToGroupAsync(Context.ConnectionId, $"item-{itemId}");
    }
}
