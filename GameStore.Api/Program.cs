using GameStore.Api.Endpoints;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(typeof(IGamesRepository<Game, int>), typeof(InMemGamesRepository));

var app = builder.Build();

app.MapGamesEndpoints();

app.Run();
