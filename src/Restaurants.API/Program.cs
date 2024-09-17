using Restaurants.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.AddBuilderExtensions();

var app = builder.Build();

await app.AddMiddleWare();

app.Run();
