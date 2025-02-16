using GameStore.Api.Models;
using System.ComponentModel.DataAnnotations;
using GameStore.Api.Data;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

var data = new GameStoreData();

app.MapGet("/games", () =>
{
    return data.GetGames()
               .Select(game => new GameSummaryDto(
                   game.Id, 
                   game.Name, 
                   game.Genre.Name, 
                   game.Price, 
                   game.ReleaseDate));
});

app.MapGet("/games/{id:guid}", (Guid id) =>
{
    var game = data.GetGame(id);

    return game is null ? Results.NotFound() : Results.Ok(
        new GameDetailsDto(
            game.Id, 
            game.Name, 
            game.Genre.Id, 
            game.Price, 
            game.ReleaseDate, 
            game.Description));
})
.WithName(GetGameEndpointName);

app.MapPost("/games", (CreateGameDto gameDto) =>
{
    var genre = data.GetGenre(gameDto.GenreId);

    if (genre is null)
    {
        return Results.BadRequest("Invalid Genre Id");
    }

    var game = new Game
    {
        Name = gameDto.Name,
        Genre = genre,
        Price = gameDto.Price,
        ReleaseDate = gameDto.ReleaseDate,
        Description = gameDto.Description
    };
    
    data.AddGame(game);
    
    var gamesResponse = new GameDetailsDto(
        game.Id,
        game.Name,
        game.Genre.Id,
        game.Price,
        game.ReleaseDate,
        game.Description
        );
    
    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, gamesResponse);
})
.WithParameterValidation();

app.MapPut("/games/{id:guid}", (Guid id, UpdateGameDto gameDto) =>
{
    var existingGame = data.GetGame(id);

    if (existingGame is null)
    {
        return Results.NotFound();
    }
    
    var genre = data.GetGenre(gameDto.GenreId);

    if (genre is null)
    {
        return Results.BadRequest("Invalid Genre Id");
    }

    existingGame.Name = gameDto.Name;
    existingGame.Genre = genre;
    existingGame.Price = gameDto.Price;
    existingGame.ReleaseDate = gameDto.ReleaseDate;
    existingGame.Description = gameDto.Description;

    return Results.NoContent(); 
})
.WithParameterValidation();

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

public record GameDetailsDto(
    Guid Id, 
    string Name, 
    Guid GenreId, 
    decimal Price,
    DateOnly ReleaseDate,
    string Description);

public record GameSummaryDto(
    Guid Id,
    string Name,
    string Genre,
    decimal Price,
    DateOnly ReleaseDate);

public record CreateGameDto(
    [Required][StringLength(50)]string Name,
    Guid GenreId,
    [Range(1, 100)]decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description);

public record UpdateGameDto(
    [Required][StringLength(50)]string Name,
    Guid GenreId,
    [Range(1, 100)]decimal Price,
    DateOnly ReleaseDate,
    [Required][StringLength(500)] string Description);

public record GenreDto(
    Guid Id, 
    string Name);
    