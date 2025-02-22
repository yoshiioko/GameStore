using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;

namespace GameStore.Api.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder app)
    {
        app.MapGet("/{id:guid}", (Guid id, GameStoreContext dbContext) =>
            {
                var game = dbContext.Games.Find(id);

                return game is null
                    ? Results.NotFound()
                    : Results.Ok(
                        new GameDetailsDto(
                            game.Id,
                            game.Name,
                            game.GenreId,
                            game.Price,
                            game.ReleaseDate,
                            game.Description));
            })
            .WithName(EndpointNames.GetGame);
    }
}