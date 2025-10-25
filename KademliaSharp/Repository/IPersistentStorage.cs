namespace KademliaSharp.Repository;

public interface IPersistentStorage<TKey, TValue>
{
    Task<bool> SaveAsync(TKey key, TValue value);
    Task<TValue?> LoadAsync(TKey key);
    Task<IEnumerable<(TKey key, TValue value)>> LoadAllAsync();
    Task RemoveAsync(TKey key);
}