using System.Numerics;
using System.Runtime.Serialization;
using KademliaSharp.Connection;
using KademliaSharp.Node;

namespace KademliaSharp.RoutingTable;

public interface IBucket<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    // int Id { get; }
    // int Size { get; }
    // bool Contains(TId id);
    // bool Contains(INode<TId, TC> node);
    //
    // void Add(IExternalNode<TId, TC> node);
    //
    // void Remove(INode<TId, TC> node);
    // void Remove(TId nodeId);
    //
    // void PushToFront(IExternalNode<TId, TC> node);
    //
    // IExternalNode<TId, TC> GetNode(TId id);
    // IReadOnlyList<TId> GetNodeIds();
}