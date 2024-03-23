using GameStore.Api.Data;
using GameStore.Api.Endpoints;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton(typeof(IGamesRepository<Game, int>), typeof(InMemGamesRepository));

var connString = builder.Configuration.GetConnectionString("GameStoreContext");
builder.Services.AddNpgsql<GameStoreContext>(connString);

var app = builder.Build();

app.Services.InitializeDb();

app.MapGamesEndpoints();

app.Run();
