using GameStore.Api.Data;
using GameStore.Api.Features.Games.Constants;

namespace GameStore.Api.Features.Games.GetGame;

public static class GetGameEndpoint
{
    public static void MapGetGame(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapGet("/games/{id:guid}", (Guid id) =>
            {
                var game = data.GetGame(id);

                return game is null
                    ? Results.NotFound()
                    : Results.Ok(
                        new GameDetailsDto(
                            game.Id,
                            game.Name,
                            game.Genre.Id,
                            game.Price,
                            game.ReleaseDate,
                            game.Description));
            })
            .WithName(EndpointNames.GetGame);
    }
}