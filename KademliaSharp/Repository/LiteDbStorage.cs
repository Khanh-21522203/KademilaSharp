using LiteDB;

namespace KademliaSharp.Repository;

public class LiteDbStorage<TKey, TValue> : IPersistentStorage<TKey, TValue>, IDisposable
    where TKey : notnull
{
    private readonly LiteDatabase _db;
    private readonly ILiteCollection<StoredEntry> _collection;

    private class StoredEntry
    {
        [BsonId]
        public string Key { get; init; } = string.Empty;
        public TValue Value { get; init; } = default!;
        public DateTime UpdatedAt { get; set; }
    }

    public LiteDbStorage(string dbPath)
    {
        _db = new LiteDatabase(dbPath);
        _collection = _db.GetCollection<StoredEntry>("originals");
        _collection.EnsureIndex(x => x.Key);
    }

    public Task<bool> SaveAsync(TKey key, TValue value)
    {
        var entry = new StoredEntry
        {
            Key = key.ToString() ?? string.Empty,
            Value = value,
            UpdatedAt = DateTime.UtcNow
        };
        
        var success =_collection.Upsert(entry);
        return Task.FromResult(success);
    }

    public Task<TValue?> LoadAsync(TKey key)
    {
        var entry = _collection.FindById(key.ToString());
        var result = entry != null ? entry.Value : default;
        return Task.FromResult(result);
    }

    public Task<IEnumerable<(TKey key, TValue value)>> LoadAllAsync()
    {
        var entries = _collection.FindAll();
        
        var results = entries
            .Select(e => (ParseKey(e.Key), e.Value))
            .ToList();
        
        return Task.FromResult<IEnumerable<(TKey key, TValue value)>>(results);
    }

    public Task RemoveAsync(TKey key)
    {
        _collection.Delete(key.ToString());
        return Task.CompletedTask;
    }

    private TKey ParseKey(string id)
    {
        if (typeof(TKey) == typeof(string))
            return (TKey)(object)id;
        
        if (typeof(TKey) == typeof(Guid))
            return (TKey)(object)Guid.Parse(id);
        
        throw new NotSupportedException($"Cannot parse key of type {typeof(TKey)}");
    }

    public void Dispose()
    {
        _db?.Dispose();
    }
}
