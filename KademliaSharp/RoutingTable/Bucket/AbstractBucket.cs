using System.Collections.Concurrent;
using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Node.RoutedNode;
using KademliaSharp.Utils.Collections;

namespace KademliaSharp.RoutingTable.Bucket;

public class AbstractBucket<TId, TC>(int id, int maxSize) : IBucket<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    private readonly int _id = id;
    private readonly int _maxSize = maxSize;
    private readonly InsertFrontCache<TId, RoutedNode<TId, TC>> _nodeMaps = new InsertFrontCache<TId, RoutedNode<TId, TC>>(maxSize);

    public int GetId() => _id;

    public int GetSize() => _maxSize;

    public bool IsContains(TId nodeId) => _nodeMaps.ContainsKey(nodeId);

    public bool TryAddNode(RoutedNode<TId, TC> node) => _nodeMaps.TryAddOrUpdate(node.GetId(), node);

    public bool TryRemoveById(TId nodeId) => _nodeMaps.TryRemove(nodeId);
    
    public bool TryGetNode(TId nodeId, out RoutedNode<TId, TC> node) => _nodeMaps.TryGet(nodeId, out node);
    
    public List<TId> GetAllNodeIds() => _nodeMaps.GetAllKeys();
}

public class BigIntBucket<TC>(int id, int maxSize) : AbstractBucket<BigInteger, TC>(id, maxSize)
    where TC : IConnection { }

public class LongBucket<TC>(int id, int maxSize) : AbstractBucket<long, TC>(id, maxSize)
    where TC : IConnection { }
    
public class IntBucket<TC>(int id, int maxSize) : AbstractBucket<int, TC>(id, maxSize)
    where TC : IConnection { }