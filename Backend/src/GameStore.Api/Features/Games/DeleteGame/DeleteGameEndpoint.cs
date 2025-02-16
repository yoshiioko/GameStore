using GameStore.Api.Data;

namespace GameStore.Api.Features.Games.DeleteGame;

public static class DeleteGameEndpoint
{
    public static void MapDeleteGame(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapDelete("/games/{id:guid}", (Guid id) =>
        {
            data.RemoveGame(id);

            return Results.NoContent();
        });
    }
}