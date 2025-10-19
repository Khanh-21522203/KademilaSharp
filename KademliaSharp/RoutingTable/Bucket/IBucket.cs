using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Node.RoutedNode;

namespace KademliaSharp.RoutingTable.Bucket;

public interface IBucket<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    int GetId();
    int GetSize();
    bool IsContains(TId nodeId);
    bool TryAddNode(RoutedNode<TId, TC> node);
    bool TryRemoveById(TId nodeId);
    public bool TryGetNode(TId nodeId, out RoutedNode<TId, TC> node);
    List<TId> GetAllNodeIds();
}