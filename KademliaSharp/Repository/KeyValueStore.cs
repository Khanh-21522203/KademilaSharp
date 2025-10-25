using System.Collections.Concurrent;
using Serilog;

namespace KademliaSharp.Repository;

public interface IKeyValueStore<TKey, TValue> 
    where TKey : notnull
{
    // Basic operations
    void SetOriginal(TKey key, TValue value);
    void Set(TKey key, TValue value, TimeSpan expiration);
    bool TryGet(TKey key, out TValue? value);
    bool Remove(TKey key);
    
    // Maintenance operations
    IEnumerable<(TKey key, TValue value)> GetKeysForRepublish(TimeSpan interval);
    void Clear();
    int Count { get; }
}

public record StoredValue<TValue>(TValue Value, DateTime? ExpiresAt, DateTime StoredAt, bool IsOriginal)
{
    public DateTime LastRepublished { get; set; } = StoredAt;
    
    public bool IsExpired => 
        !IsOriginal &&
        ExpiresAt.HasValue && 
        DateTime.UtcNow >= ExpiresAt.Value;
}

public class KeyValueStore<TKey, TValue> : IKeyValueStore<TKey, TValue>, IDisposable
    where TKey : notnull
    where TValue : notnull
{
    private readonly ConcurrentDictionary<TKey, StoredValue<TValue>> _storage;
    private readonly Timer _cleanupTimer;
    private readonly ILogger _logger;
    
    public KeyValueStore(ILogger logger)
    {
        _logger = logger;
        _storage = new ConcurrentDictionary<TKey, StoredValue<TValue>>();
        _cleanupTimer = new Timer(CleanupExpiredEntries, null, TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5));
    }
    
    public int Count => _storage.Count(kvp => !kvp.Value.IsExpired);

    public void SetOriginal(TKey key, TValue value)
    {
        var now = DateTime.UtcNow;
    
        _storage.AddOrUpdate(
            key,
            _ => new StoredValue<TValue>(
                Value: value,
                ExpiresAt: null,
                StoredAt: now,
                IsOriginal: true
            ),
            (_, existing) => new StoredValue<TValue>(
                Value: value,
                ExpiresAt: null,
                StoredAt: existing.StoredAt,
                IsOriginal: true
            )
        );
    
        _logger.Information("Stored original key {Key}", key);
    }

    public void Set(TKey key, TValue value, TimeSpan expiration)
    {
        var now = DateTime.UtcNow;
        
        _storage.AddOrUpdate(
            key,
            _ => new StoredValue<TValue>(
                Value: value,
                ExpiresAt: now.Add(expiration),
                StoredAt: now,
                IsOriginal: false
            ),
            (_, existing) => new StoredValue<TValue>(
                Value: value,
                ExpiresAt: now.Add(expiration),
                StoredAt: existing.StoredAt,
                IsOriginal: false
            )
        );
        
        _logger.Information("Stored key {Key} with expiration {Expiration}", key, expiration);
    }

    public bool TryGet(TKey key, out TValue? value)
    {
        value = default;

        if (_storage.TryGetValue(key, out var storedValue))
        {
            if (storedValue.IsExpired)
            {
                _storage.TryRemove(key, out _);
                _logger.Information("Try get key {Key} failed: entry expired", key);
                return false;
            }
            value = storedValue.Value;
            return true;
        }
        return false;
    }

    public bool Remove(TKey key)
    {
        if (_storage.TryRemove(key, out _))
        {
            _logger.Information("Removed key {Key}", key);
            return true;
        }
        return false;
    }

    public IEnumerable<(TKey key, TValue value)> GetOriginalKeysForRepublish(
        TimeSpan interval)
    {
        var cutoffTime = DateTime.UtcNow.Subtract(interval);
        
        return _storage
            .Where(kvp => kvp.Value.IsOriginal)
            .Where(kvp => kvp.Value.LastRepublished <= cutoffTime)
            .Select(kvp =>
            {
                kvp.Value.LastRepublished = DateTime.UtcNow;
                return (kvp.Key, kvp.Value.Value);
            });
    }
    
    public IEnumerable<(TKey key, TValue value)> GetKeysForRepublish(TimeSpan interval)
    {
        var cutoffTime = DateTime.UtcNow.Subtract(interval);
        
        return _storage
            .Where(kvp => !kvp.Value.IsExpired)
            .Where(kvp => kvp.Value.LastRepublished <= cutoffTime)
            .Select(kvp =>
            {
                kvp.Value.LastRepublished = DateTime.UtcNow;
                return (kvp.Key, kvp.Value.Value);
            });
    }

    public void Clear()
    {
        var count = _storage.Count;
        _storage.Clear();
        _logger.Information("Cleared {Count} entries", count);    
    }
    
    private void CleanupExpiredEntries(object? state)
    {
        var expiredKeys = _storage
            .Where(kvp => kvp.Value.IsExpired)
            .Select(kvp => kvp.Key)
            .ToList();

        foreach (var key in expiredKeys)
        {
            _storage.TryRemove(key, out _);
        }

        if (expiredKeys.Count > 0)
        {
            _logger.Information("Cleaned up {Count} expired entries", expiredKeys.Count);
        }
    }
    
    public void Dispose()
    {
        _cleanupTimer?.Dispose();
    }
}