using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// add services to the container 
builder.Services.AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddApi();

var app = builder.Build();

// Configure the http pipe line 

app.Run();
