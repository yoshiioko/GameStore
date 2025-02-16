using GameStore.Api.Data;
using GameStore.Api.Features.Games.CreateGame;
using GameStore.Api.Features.Games.GetGame;
using GameStore.Api.Features.Games.GetGames;
using GameStore.Api.Features.Games.UpdateGame;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

var data = new GameStoreData();

app.MapGetGames(data);
app.MapGetGame(data);
app.MapCreateGame(data);
app.MapUpdateGame(data);

app.MapDelete("/games/{id:guid}", (Guid id) =>
{
    data.RemoveGame(id);

    return Results.NoContent();
});

app.MapGet("/genres", () =>
{
    return data.GetGenres()
        .Select(genre => new GenreDto(genre.Id, genre.Name));
});

app.Run();


public record GenreDto(
    Guid Id,
    string Name);