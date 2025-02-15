using GameStore.Api.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

const string GetGameEndpointName = "GetGame";

List<Genre> genres = 
[
    new() {Id = new Guid("1319fe82-459d-4eaa-b5fb-0f67357f9037"), Name = "Fighting"}, 
    new() {Id = new Guid("fc44dbe1-198b-4b8f-9a3f-8c5ba556b1e1"), Name = "Kids and Family"}, 
    new() {Id = new Guid("270d8959-4445-4996-99ef-f2a6d9346d35"), Name = "Racing"}, 
    new() {Id = new Guid("c7d2dd03-ddfe-4557-84a1-62f0eaf2c105"), Name = "Roleplaying"}, 
    new() {Id = new Guid("fc0237d0-0330-4899-978b-6d381493932e"), Name = "Sports"},
];

List<Game> games =
[
    new()
    {
        Id = Guid.NewGuid(),
        Name = "Street Fighter II",
        Genre = genres[0],
        Price = 19.99m,
        ReleaseDate = new DateOnly(1992, 7, 15),
        Description = "Street Fighter II is regarded as one of the greatest video games of all time and the most important and influential fighting game ever made."
    },
    new()
    {
        Id = Guid.NewGuid(),
        Name = "Final Fantasy XIV",
        Genre = genres[3],
        Price = 59.99m,
        ReleaseDate = new DateOnly(2010, 9, 30),
        Description = "Final Fantasy XIV is a massively multiplayer online role-playing game (MMORPG) developed and published by Square Enix."
    },
    new()
    {
        Id = Guid.NewGuid(),
        Name = "FIFA 23",
        Genre = genres[4],
        Price = 69.99m,
        ReleaseDate = new DateOnly(2022, 9, 27),
        Description = "FIFA 23 is a football video game published by EA Sports."
    }
];

app.MapGet("/games", () => games);

app.MapGet("/games/{id:guid}", (Guid id) =>
{
    var game = games.Find(game => game.Id == id);

    return game is null ? Results.NotFound() : Results.Ok(game);
})
.WithName(GetGameEndpointName);

app.MapPost("/games", (Game game) =>
{
    game.Id = Guid.NewGuid();
    games.Add(game);

    return Results.CreatedAtRoute(GetGameEndpointName, new { id = game.Id }, game);
})
.WithParameterValidation();

app.MapPut("/games/{id:guid}", (Guid id, Game updatedGame) =>
{
    var existingGame = games.Find(game => game.Id == id);

    if (existingGame is null)
    {
        return Results.NotFound();
    }

    existingGame.Name = updatedGame.Name;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;

    return Results.NoContent();
})
.WithParameterValidation();

app.MapDelete("/games/{id:guid}", (Guid id) =>
{
    games.RemoveAll(game => game.Id == id);

    return Results.NoContent();
});

app.Run();
