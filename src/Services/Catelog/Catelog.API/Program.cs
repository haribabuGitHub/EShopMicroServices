using BuildingBlocks.Behaviours;
using BuildingBlocks.Exceptions.Handler;
using Catelog.API.Products.CreateProduct;
using FluentValidation;


var builder = WebApplication.CreateBuilder(args);
//add the services to the container
builder.Services.AddLogging();

// Keep Carter's validation integration; do not call non-existent members on ValidationOptions.
builder.Services.AddValidation(config =>
{
    // If you need to customize Carter's ValidationOptions, do it here.
    // Do NOT call config.RegisterValidatorsFromAssembly<Program>() — that member doesn't exist.
});

// Register FluentValidation validators from this assembly using the IServiceCollection extension.

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(typeof(Program).Assembly);
    config.AddOpenBehavior(typeof(ValidationBehaviour<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddCarter();


builder.Services.AddScoped<IRequestHandler<CreateProductCommand, CreateProductResult>, CreateProductCommandHandler>();
builder.Services.AddMarten(opts=> {
    opts.Connection(builder.Configuration.GetConnectionString("DefaultConnection"));
}).UseLightweightSessions();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(options => { });
app.Run();
