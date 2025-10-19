using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Model;
using KademliaSharp.Node;
using KademliaSharp.Node.RoutedNode;
using KademliaSharp.RoutingTable.Bucket;

namespace KademliaSharp.RoutingTable;

public interface IRoutingTable<TId, TC, TB>
    where TId : INumber<TId>
    where TC : IConnection
    where TB : IBucket<TId, TC>
{
    int GetNodePrefix(TId nodeId);
    IBucket<TId, TC> GetNode(TId nodeId);
    bool TryUpdateNode(INode<TId, TC> node);
    void ForceUpdateNode(INode<TId, TC> node);
    bool TryRemoveNode(INode<TId, TC> node);
    FindNodeAnswer<TId, TC> FindClosestNodes(TId targetId);
    bool IsContains(TId targetId);
    List<TB> GetAllBuckets();
    TId GetDistance(TId nodeId);
    RoutedNode<TId, TC> GetRoutedNode(INode<TId, TC> node);
}