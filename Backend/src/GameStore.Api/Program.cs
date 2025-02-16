using GameStore.Api.Data;
using GameStore.Api.Features.Games;
using GameStore.Api.Features.Genres;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var data = new GameStoreData();

app.MapGames(data);
app.MapGenres(data);

app.Run();