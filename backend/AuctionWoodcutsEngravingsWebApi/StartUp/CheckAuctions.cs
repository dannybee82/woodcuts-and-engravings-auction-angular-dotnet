using Application.BackgroundJobs;
using Application.Services;
using Domain.Enums;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace AuctionWoodcutsEngravingsWebApi.StartUp
{
    public class CheckAuctions : IHostedService
    {
        private readonly IServiceProvider _services;

        public CheckAuctions(IServiceProvider services) => _services = services;

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();            
            var scheduler = scope.ServiceProvider.GetRequiredService<IBackgroundJobScheduler>();            

            var allAuctions = await db.AuctionItems.AsNoTracking().ToListAsync().ConfigureAwait(false);
            List<int> wonAuctions = await db.AuctionWonItems.Select(x => x.AuctionItemId).ToListAsync().ConfigureAwait(false);
            List<int> closedAuctions = allAuctions.Where(x => x.Status == AuctionStatus.CLOSED).Select(x => x.Id).ToList();

            // When all Auctions are expired -> reopen closed auctions that are not won.
            if (allAuctions.Count() >= wonAuctions.Count() + closedAuctions.Count())
            {
                foreach (var auction in allAuctions)
                {
                    if(!wonAuctions.Contains(auction.Id))
                    {
                        auction.Status = AuctionStatus.OPEN;
                        var now = DateTimeOffset.UtcNow;
                        Random random = new Random();
                        int randomInt = random.Next(3, 14);
                        auction.EndsAt = now.AddDays(randomInt);
                        
                        db.AuctionItems.Update(auction);
                        await db.SaveChangesAsync();

                        scheduler.Schedule<AuctionCloser>(x => x.CloseAuction(auction.Id), auction.EndsAt);
                    }
                }
            }           
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
