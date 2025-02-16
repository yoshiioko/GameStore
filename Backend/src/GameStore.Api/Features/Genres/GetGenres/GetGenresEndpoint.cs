using GameStore.Api.Data;

namespace GameStore.Api.Features.Genres.GetGenres;

public static class GetGenresEndpoint
{
    public static void MapGetGenres(this IEndpointRouteBuilder app, GameStoreData data)
    {
        app.MapGet("/", () =>
        {
            return data.GetGenres()
                .Select(genre => new GenreDto(genre.Id, genre.Name));
        });
    }
}