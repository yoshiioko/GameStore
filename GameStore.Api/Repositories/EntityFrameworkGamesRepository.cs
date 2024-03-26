using GameStore.Api.Data;
using GameStore.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Api.Repositories;

public class EntityFrameworkGamesRepository : IGamesRepository<Game, int>
{
    private readonly GameStoreContext dbContext;
    private readonly ILogger<EntityFrameworkGamesRepository> logger;

    public EntityFrameworkGamesRepository(GameStoreContext dbContext, ILogger<EntityFrameworkGamesRepository> logger)
    {
        this.dbContext = dbContext;
        this.logger = logger;
    }

    public async Task CreateAsync(Game game)
    {
        dbContext.Games.Add(game); // Asks EF to keep track of new Entity
        await dbContext.SaveChangesAsync(); // Changes are sent to DB (inserted)

        logger.LogInformation($"Created game {game.Name} with price {game.Price}.");
    }

    public async Task DeleteAsync(int id)
    {
        await dbContext.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
    }

    public async Task<Game?> GetAsync(int id)
    {
        return await dbContext.Games.FindAsync(id);
    }

    public async Task<IEnumerable<Game>> GetAllAsync()
    {
        // return dbContext.Games.AsNoTracking().ToList();
        return await dbContext.Games.AsNoTracking().ToListAsync();
    }

    public async Task UpdateAsync(Game updateGame)
    {
        dbContext.Update(updateGame);
        await dbContext.SaveChangesAsync();
    }
}
