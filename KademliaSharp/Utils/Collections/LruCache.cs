namespace KademliaSharp.Utils.Collections;

public class LruCache<TKey, TValue> where TKey : notnull
{
    private readonly int _capacity;
    private readonly LinkedList<KeyValuePair<TKey, TValue>> _lruList;
    private readonly Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>> _map;

    public LruCache(int capacity)
    {
        if (capacity <= 0) throw new ArgumentOutOfRangeException(nameof(capacity), "Capacity must be > 0");
        _capacity = capacity;
        _map = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, TValue>>>(capacity);
        _lruList = new LinkedList<KeyValuePair<TKey, TValue>>();
    }

    public int Count => _map.Count;

    public TValue Get(TKey key)
    {
        if (TryGet(key, out var value))
            return value;
        throw new KeyNotFoundException($"Key not found: {key}");
    }

    public bool TryGet(TKey key, out TValue value)
    {
        if (_map.TryGetValue(key, out var node))
        {
            // Move to front (most recently used)
            _lruList.Remove(node);
            _lruList.AddFirst(node);
            value = node.Value.Value;
            return true;
        }

        value = default!;
        return false;
    }

    public IReadOnlyCollection<TValue> GetValues()
    {
        return _lruList
            .Select(kv => kv.Value)
            .ToList()
            .AsReadOnly();
    }

    public void Put(TKey key, TValue value)
    {
        if (_map.TryGetValue(key, out var existingNode))
        {
            // Update and move to front
            _lruList.Remove(existingNode);
            var newNode = new LinkedListNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, value));
            _lruList.AddFirst(newNode);
            _map[key] = newNode;
        }
        else
        {
            // Add new
            var node = new LinkedListNode<KeyValuePair<TKey, TValue>>(new KeyValuePair<TKey, TValue>(key, value));
            _lruList.AddFirst(node);
            _map.Add(key, node);

            // Evict if needed
            if (_map.Count > _capacity)
            {
                var lru = _lruList.Last!;
                _lruList.RemoveLast();
                _map.Remove(lru.Value.Key);
            }
        }
    }

    public void Update(TKey key)
    {
        if (_map.TryGetValue(key, out var node))
        {
            // Move to front (most recently used)
            _lruList.Remove(node);
            _lruList.AddFirst(node);
        }
    }

    public bool Contains(TKey key)
    {
        return _map.ContainsKey(key);
    }

    public bool Remove(TKey key)
    {
        if (_map.TryGetValue(key, out var node))
        {
            _lruList.Remove(node);
            _map.Remove(key);
            return true;
        }

        return false;
    }

    public void Clear()
    {
        _map.Clear();
        _lruList.Clear();
    }
}