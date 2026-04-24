using Discount.Grpc.Data;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddDbContext<Discount.Grpc.Data.DiscountContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Database")));

builder.WebHost.ConfigureKestrel(options =>
{
    // HTTP/2 endpoint (gRPC over HTTPS)
    options.ListenLocalhost(5052, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http2;
    });

    // HTTP endpoint (for non-gRPC traffic)
    options.ListenLocalhost(5002, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
   
});

var app = builder.Build();

// Ensure database directory exists before migration
var dataDirectory = Path.Combine(AppContext.BaseDirectory, "data");
Directory.CreateDirectory(dataDirectory);

app.UseMigration();

app.MapGrpcService<Discount.Grpc.Services.DiscountService>();

// Configure the HTTP request pipeline.
app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
