using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Model;
using KademliaSharp.Node;
using KademliaSharp.Node.RoutedNode;
using KademliaSharp.RoutingTable.Bucket;

namespace KademliaSharp.RoutingTable;

public class AbstractRoutingTable<TId, TC, TB>(TId localNodeId) : IRoutingTable<TId, TC, TB>
    where TId : INumber<TId>
    where TC : IConnection
    where TB : IBucket<TId, TC>
{
    private readonly TId _localNodeId = localNodeId;
    private readonly List<TB> _buckets = new();

    public int GetNodePrefix(TId nodeId)
    {
        throw new NotImplementedException();
    }

    public IBucket<TId, TC> GetNode(TId nodeId)
    {
        throw new NotImplementedException();
    }

    public bool TryUpdateNode(INode<TId, TC> node)
    {
        throw new NotImplementedException();
    }

    public void ForceUpdateNode(INode<TId, TC> node)
    {
        throw new NotImplementedException();
    }

    public bool TryRemoveNode(INode<TId, TC> node)
    {
        throw new NotImplementedException();
    }

    public FindNodeAnswer<TId, TC> FindClosestNodes(TId targetId)
    {
        throw new NotImplementedException();
    }

    public bool IsContains(TId targetId)
    {
        throw new NotImplementedException();
    }

    public List<TB> GetAllBuckets() => _buckets;

    public TId GetDistance(TId nodeId)
    {
        throw new NotImplementedException();
    }

    public RoutedNode<TId, TC> GetRoutedNode(INode<TId, TC> node)
    {
        throw new NotImplementedException();
    }
}