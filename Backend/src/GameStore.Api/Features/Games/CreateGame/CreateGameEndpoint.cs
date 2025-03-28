using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;
using GameStore.Api.Models;

namespace GameStore.Api.Features.Games.CreateGame;

public static class CreateGameEndpoint
{
    public static void MapCreateGame(this IEndpointRouteBuilder app)
    {
        app.MapPost("/", async (CreateGameDto gameDto, GameStoreContext dbContext, ILogger<Program> logger) =>
            {
                var game = new Game
                {
                    Name = gameDto.Name,
                    GenreId = gameDto.GenreId,
                    Price = gameDto.Price,
                    ReleaseDate = gameDto.ReleaseDate,
                    Description = gameDto.Description
                };

                await dbContext.Games.AddAsync(game);
                await dbContext.SaveChangesAsync();
                
                logger.LogInformation("Created game {GameName} with price {GamePrice}", game.Name, game.Price);

                var gamesResponse = new GameDetailsDto(
                    game.Id,
                    game.Name,
                    game.GenreId,
                    game.Price,
                    game.ReleaseDate,
                    game.Description
                );

                return Results.CreatedAtRoute(EndpointNames.GetGame, new { id = game.Id }, gamesResponse);
            })
            .WithParameterValidation();
    }
}