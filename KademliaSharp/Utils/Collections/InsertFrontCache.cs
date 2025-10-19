namespace KademliaSharp.Utils.Collections;

public sealed class InsertFrontCache<TK, TV>
    where TK : notnull
{
    private readonly int _capacity;
    private readonly Lock _lock = new();
    private readonly Dictionary<TK, LinkedListNode<(TK key, TV value)>> _map;
    private readonly LinkedList<(TK key, TV value)> _order;

    public InsertFrontCache(int capacity)
    {
        ArgumentOutOfRangeException.ThrowIfNegativeOrZero(capacity);
        
        _capacity = capacity;
        _map = new Dictionary<TK, LinkedListNode<(TK, TV)>>(capacity);
        _order = [];
    }

    public int Capacity => _capacity;

    public int Count
    {
        get { lock (_lock) return _map.Count; }
    }

    public bool TryAddOrUpdate(TK key, TV value, bool updateIfExists = true, bool promoteOnUpdate = true)
    {
        lock (_lock)
        {
            if (_map.TryGetValue(key, out var node))
            {
                if (!updateIfExists)
                    return false;

                node.Value = (key, value);

                if (promoteOnUpdate && node != _order.First)
                {
                    _order.Remove(node);
                    _order.AddFirst(node);
                }
                return true;
            }

            if (_map.Count >= _capacity)
            {
                var last = _order.Last!;
                _order.RemoveLast();
                _map.Remove(last.Value.key);
            }

            var newNode = new LinkedListNode<(TK, TV)>((key, value));
            _order.AddFirst(newNode);
            _map[key] = newNode;
            return true;
        }
    }

    public bool TryGet(TK key, out TV value)
    {
        lock (_lock)
        {
            if (_map.TryGetValue(key, out var node))
            {
                value = node.Value.value;
                return true;
            }
            value = default!;
            return false;
        }
    }

    public bool ContainsKey(TK key)
    {
        lock (_lock) return _map.ContainsKey(key);
    }

    public bool TryRemove(TK key)
    {
        lock (_lock)
        {
            if (_map.TryGetValue(key, out var node))
            {
                _order.Remove(node);
                _map.Remove(key);
                return true;
            }
            return false;
        }
    }

    public List<TK> GetAllKeys()
    {
        lock (_lock)
        {
            var values = new List<TK>(_map.Count);
            foreach (var node in _order)
            {
                values.Add(node.key);
            }
            return values;
        }
    }
}