using System.Diagnostics;
using GameStore.Api.Authorization;
using GameStore.Api.Dtos;
using GameStore.Api.Entities;
using GameStore.Api.Repositories;

namespace GameStore.Api.Endpoints;

public static class GamesEndpoints
{
    const string GetGameV1EndpointName = "GetGameV1";
    const string GetGameV2EndpointName = "GetGameV2";

    public static RouteGroupBuilder MapGamesEndpoints(this IEndpointRouteBuilder routes)
    {
        var v1Group = routes.MapGroup("/v1/games").WithParameterValidation();

        var v2Group = routes.MapGroup("/v2/games").WithParameterValidation();

        // V1 Get Endpoints
        v1Group.MapGet("/", async (IGamesRepository<Game, int> repository, ILoggerFactory loggerFactory) =>
        {
            return Results.Ok((await repository.GetAllAsync()).Select(game => game.AsDtoV1()));
        });

        v1Group.MapGet("/{id}", async (IGamesRepository<Game, int> repository, int id) =>
        {
            Game? game = await repository.GetAsync(id);

            return game is not null ? Results.Ok(game.AsDtoV1()) : Results.NotFound();
        })
        .WithName(GetGameV1EndpointName)
        .RequireAuthorization(Policies.ReadAccess);

        // V2 Get Endpoints
        v2Group.MapGet("/", async (IGamesRepository<Game, int> repository, ILoggerFactory loggerFactory) =>
        {
            return Results.Ok((await repository.GetAllAsync()).Select(game => game.AsDtoV2()));
        });

        v2Group.MapGet("/{id}", async (IGamesRepository<Game, int> repository, int id) =>
        {
            Game? game = await repository.GetAsync(id);

            return game is not null ? Results.Ok(game.AsDtoV2()) : Results.NotFound();
        })
        .WithName(GetGameV2EndpointName)
        .RequireAuthorization(Policies.ReadAccess);

        v1Group.MapPost("/", async (IGamesRepository<Game, int> repository, CreateGameDto gameDto) =>
        {
            // Convert from DTO to Entity
            Game game = new()
            {
                Name = gameDto.Name,
                Genre = gameDto.Genre,
                Price = gameDto.Price,
                ReleaseDate = gameDto.ReleaseDate.ToUniversalTime(),
                ImageUri = gameDto.ImageUri
            };

            await repository.CreateAsync(game);

            return Results.CreatedAtRoute(GetGameV1EndpointName, new { id = game.Id }, game);
        })
        .RequireAuthorization(Policies.WriteAccess);

        v1Group.MapPut("/{id}", async (IGamesRepository<Game, int> repository, int id, UpdateGameDto updatedGameDto) =>
        {
            Game? existingGame = await repository.GetAsync(id);

            if (existingGame is null)
            {
                return Results.NotFound();
            }

            // Convert from DTO to Entity
            existingGame.Name = updatedGameDto.Name;
            existingGame.Genre = updatedGameDto.Genre;
            existingGame.Price = updatedGameDto.Price;
            existingGame.ReleaseDate = updatedGameDto.ReleaseDate.ToUniversalTime();
            existingGame.ImageUri = updatedGameDto.ImageUri;

            await repository.UpdateAsync(existingGame);

            return Results.NoContent();
        })
        .RequireAuthorization(Policies.WriteAccess);

        v1Group.MapDelete("/{id}", async (IGamesRepository<Game, int> repository, int id) =>
        {
            Game? game = await repository.GetAsync(id);

            if (game is not null)
            {
                await repository.DeleteAsync(id);
            }

            return Results.NoContent();
        })
        .RequireAuthorization(Policies.WriteAccess);

        return v1Group;
    }
}
