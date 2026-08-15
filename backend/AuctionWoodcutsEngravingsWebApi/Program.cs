using Application.BackgroundJobs;
using Application.Services;
using Application.Settings;
using AuctionWoodcutsEngravingsWebApi.Hangfire;
using AuctionWoodcutsEngravingsWebApi.hubs;
using AuctionWoodcutsEngravingsWebApi.StartUp;
using Domain.Interfaces;
using Domain.Settings;
using FileSignatures;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.Transactions;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//For PostgreSQL Databases and DateTime.
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add DbContext.
var dbConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Check — null connection strings produce cryptic format errors  
if (string.IsNullOrWhiteSpace(dbConnectionString))
    throw new InvalidOperationException(
        "ConnectionStrings:DefaultConnection is not set. " +
        "Check your appsettings.LocalDocker.json or environment variables.");

builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dbConnectionString));

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

// Enable CORS - Cross Origin Resource Sharing
// Note: "http://localhost" = for Docker Desktop.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowOrigin", policy =>
        policy.WithOrigins("http://localhost:4200", "https://localhost:4200", "http://localhost") 
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials());   // <-- the missing piece
});

// Hangfire
builder.Services.AddHangfire(configuration => configuration
        .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
        .UseSimpleAssemblyNameTypeSerializer()
        .UseRecommendedSerializerSettings()
        .UsePostgreSqlStorage(options => options.UseNpgsqlConnection(dbConnectionString)));

// Add the processing server as IHostedService
builder.Services.AddHangfireServer();

// SignalR
// Add SignalR services
builder.Services.AddSignalR(options =>
{
    options.EnableDetailedErrors = builder.Environment.IsDevelopment();
    options.MaximumReceiveMessageSize = 32 * 1024; // 32KB
    options.StreamBufferCapacity = 10;
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);
});

// Set allowed File Types. See: FileSignatures.Formats for available formats.
//Also see: https://github.com/neilharvey/FileSignatures/
var allowedFileList = new AllowedFileFormatList()
{
    AllowedFileList = new List<AllowedFileFormat>()
    {
        new AllowedFileFormat()
        {
            Extension = "jpg",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.Jpeg>()
        },
        new AllowedFileFormat()
        {
            Extension = "jpeg",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.Jpeg>()
        },
        new AllowedFileFormat()
        {
            Extension = "png",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.Png>()
        },
        new AllowedFileFormat()
        {
            Extension = "gif",
            FileFormat = FileFormatLocator.GetFormats().OfType<FileSignatures.Formats.Gif>()
        }
    }
};

// Repositories
builder.Services.AddScoped<IAuctionItemRepository, AuctionItemRepository>();
builder.Services.AddScoped<IAuctionBidRepository, AuctionBidRepository>();
builder.Services.AddScoped<IAuctionItemImageRepository, AuctionItemImageRepository>();
builder.Services.AddScoped<IAuctionItemDetailRepository, AuctionItemDetailRepository>();
builder.Services.AddScoped<IAuctionWonItemRepository, AuctionWonItemRepository>();
builder.Services.AddScoped<IDummyUserRepository, DummyUserRepository>();

// Transactions
builder.Services.AddScoped<IAuctionBidTransaction, AuctionBidTransaction>();
builder.Services.AddScoped<IAuctionDeleteTransaction, AuctionDeleteTransaction>();

// Services
builder.Services.AddScoped<IAuctionItemService, AuctionItemService>();
builder.Services.AddScoped<IAuctionBidService, AuctionBidService>();
builder.Services.AddScoped<IAuctionWonItemService, AuctionWonItemService>();
builder.Services.AddScoped<IBackgroundJobScheduler, HangfireJobScheduler>();
builder.Services.AddScoped<IHelpersService, HelpersService>();

// Allowed files.
builder.Services.AddSingleton<IAllowedFileFormatList, AllowedFileFormatList>((IServiceProvider sp) => allowedFileList);
builder.Services.AddSingleton<IFileFormatInspector>(new FileFormatInspector());

// Update auctions after server shut-down, etc.
builder.Services.AddHostedService<AuctionCatchupService>();
// When all Auctions are expired -> reopen closed auctions that are not won.
builder.Services.AddHostedService<CheckAuctions>();

builder.Services.AddControllers();

// CultureInfo = this is needed for decimals.
CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
    app.MapOpenApi();
    app.MapScalarApiReference();   // UI at /scalar/v1
//}

app.UseRouting();
app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthorization();

// Map SignalR hubs
app.MapHub<AuctionHub>("/hubs/auction");

app.MapControllers();

app.Run();
