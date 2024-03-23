using GameStore.Api.Data;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class EntityFrameworkGamesRepository : IGamesRepository<Game, int>
{
    private readonly GameStoreContext dbContext;

    public EntityFrameworkGamesRepository(GameStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public void Create(Game game)
    {
        dbContext.Games.Add(game); // Asks EF to keep track of new Entity
        dbContext.SaveChanges(); // Changes are sent to DB (inserted)
    }

    public void Delete(int id)
    {
        dbContext.Games.Where(game => game.Id == id).ExecuteDelete();
    }

    public Game? Get(int id)
    {
        return dbContext.Games.Find(id);
    }

    public IEnumerable<Game> GetAll()
    {
        // return dbContext.Games.AsNoTracking().ToList();
        return [.. dbContext.Games.AsNoTracking()];
    }

    public void Update(Game updateGame)
    {
        dbContext.Update(updateGame);
        dbContext.SaveChanges();
    }
}
