using Domain.Enums;
using Infrastructure.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Application.Services;
using Application.BackgroundJobs;

namespace AuctionWoodcutsEngravingsWebApi.Hangfire
{
    public class AuctionCatchupService : IHostedService
    {
        private readonly IServiceProvider _services;

        public AuctionCatchupService(IServiceProvider services) => _services = services;

        public async Task StartAsync(CancellationToken ct)
        {
            using var scope = _services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var scheduler = scope.ServiceProvider.GetRequiredService<IBackgroundJobScheduler>();

            var now = DateTimeOffset.UtcNow;

            // Auctions already past end time but still open -> close now
            var overdue = await db.AuctionItems
                .Where(i => i.Status == AuctionStatus.OPEN && i.EndsAt <= now)
                .Select(i => i.Id)
                .ToListAsync(ct);

            foreach (var id in overdue)
                scheduler.Enqueue<AuctionCloser>(x => x.CloseAuction(id)); // run immediately

            // Auctions still in the future -> (re)schedule their close jobs
            var upcoming = await db.AuctionItems
                .Where(i => i.Status == AuctionStatus.OPEN && i.EndsAt > now)
                .Select(i => new { i.Id, i.EndsAt })
                .ToListAsync(ct);

            foreach (var a in upcoming)
                scheduler.Schedule<AuctionCloser>(x => x.CloseAuction(a.Id), a.EndsAt);
        }

        public Task StopAsync(CancellationToken ct) => Task.CompletedTask;
    }
}