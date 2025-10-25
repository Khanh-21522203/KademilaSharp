namespace KademliaSharp.Utils.Collections;

public class ConcurrentLruCache<TKey, TValue>(int capacity)
    where TKey : notnull
{
    private readonly LruCache<TKey, TValue> _inner = new(capacity);
    private readonly Lock _sync = new();

    public int Count
    {
        get
        {
            lock (_sync)
            {
                return _inner.Count;
            }
        }
    }

    public TValue Get(TKey key)
    {
        lock (_sync)
        {
            return _inner.Get(key);
        }
    }

    public bool TryGet(TKey key, out TValue value)
    {
        lock (_sync)
        {
            return _inner.TryGet(key, out value);
        }
    }

    public IReadOnlyCollection<TValue> GetValues()
    {
        lock (_sync)
        {
            return _inner.GetValues();
        }
    }

    public void Put(TKey key, TValue value)
    {
        lock (_sync)
        {
            _inner.Put(key, value);
        }
    }

    public void Update(TKey key)
    {
        lock (_sync)
        {
            if (_inner.TryGet(key, out var value)) _inner.Put(key, value);
        }
    }

    public bool Contains(TKey key)
    {
        lock (_sync)
        {
            return _inner.Contains(key);
        }
    }

    public bool Remove(TKey key)
    {
        lock (_sync)
        {
            return _inner.Remove(key);
        }
    }

    public void Clear()
    {
        lock (_sync)
        {
            _inner.Clear();
        }
    }
}