var builder = WebApplication.CreateBuilder(args);
//add the services to the container


var app = builder.Build();

// configure the http request pipeline

app.Run();
