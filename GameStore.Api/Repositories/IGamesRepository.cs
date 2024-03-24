namespace GameStore.Api.Repositories;

public interface IGamesRepository<T, U>
{
    Task CreateAsync(T game);
    Task DeleteAsync(U id);
    Task<T?> GetAsync(U id);
    Task<IEnumerable<T>> GetAllAsync();
    Task UpdateAsync(T updateGame);
}
