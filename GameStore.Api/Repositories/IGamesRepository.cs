namespace GameStore.Api.Repositories;

public interface IGamesRepository<T, U>
{
    void Create(T game);
    void Delete(U id);
    T? Get(U id);
    IEnumerable<T> GetAll();
    void Update(T updateGame);
}
