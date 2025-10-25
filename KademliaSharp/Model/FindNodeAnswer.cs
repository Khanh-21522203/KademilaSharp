using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Node;
using KademliaSharp.Node.RoutedNode;

namespace KademliaSharp.Model;

public abstract record FindNodeAnswer<TId, TC>(bool IsAlive, List<RoutedNode<TId, TC>> Nodes)
    where TId : INumber<TId>
    where TC : IConnection
{
    
}