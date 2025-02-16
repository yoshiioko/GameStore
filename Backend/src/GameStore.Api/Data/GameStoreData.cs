using GameStore.Api.Models;

namespace GameStore.Api.Data;

public class GameStoreData
{
    private readonly List<Genre> _genres = 
    [
        new() {Id = new Guid("1319fe82-459d-4eaa-b5fb-0f67357f9037"), Name = "Fighting"}, 
        new() {Id = new Guid("fc44dbe1-198b-4b8f-9a3f-8c5ba556b1e1"), Name = "Kids and Family"}, 
        new() {Id = new Guid("270d8959-4445-4996-99ef-f2a6d9346d35"), Name = "Racing"}, 
        new() {Id = new Guid("c7d2dd03-ddfe-4557-84a1-62f0eaf2c105"), Name = "Roleplaying"}, 
        new() {Id = new Guid("fc0237d0-0330-4899-978b-6d381493932e"), Name = "Sports"},
    ];

    private readonly List<Game> _games;

    public GameStoreData()
    {
        _games = 
        [
            new Game
            {
                Id = Guid.NewGuid(),
                Name = "Street Fighter II",
                Genre = _genres[0],
                Price = 19.99m,
                ReleaseDate = new DateOnly(1992, 7, 15),
                Description = "Street Fighter II is regarded as one of the greatest video games of all time and the most important and influential fighting game ever made."
            },
            new Game
            {
                Id = Guid.NewGuid(),
                Name = "Final Fantasy XIV",
                Genre = _genres[3],
                Price = 59.99m,
                ReleaseDate = new DateOnly(2010, 9, 30),
                Description = "Final Fantasy XIV is a massively multiplayer online role-playing game (MMORPG) developed and published by Square Enix."
            },
            new Game
            {
                Id = Guid.NewGuid(),
                Name = "FIFA 23",
                Genre = _genres[4],
                Price = 69.99m,
                ReleaseDate = new DateOnly(2022, 9, 27),
                Description = "FIFA 23 is a football video game published by EA Sports."
            }
        ];
    }

    public IEnumerable<Game> GetGames() => _games;

    public Game? GetGame(Guid id) => _games.Find(game => game.Id == id);

    public void AddGame(Game game)
    {
        game.Id = Guid.NewGuid();
        _games.Add(game);
    }

    public void RemoveGame(Guid id)
    {
        _games.RemoveAll(game => game.Id == id);
    }

    public IEnumerable<Genre> GetGenres() => _genres;

    public Genre? GetGenre(Guid id) => _genres.Find(genre => genre.Id == id);
}
