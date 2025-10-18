using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Node.NodeAPI;

namespace KademliaSharp.Node.Strategies;

public interface IReferencedNodesStrategy
{
    List<INode<TId, TC>> GetReferencedNodes<TId, TC>(IKademliaNodeApi<TId, TC> kademliaNode)
        where TId : INumber<TId>
        where TC : IConnection;
}