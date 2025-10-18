using System.Numerics;
using System.Runtime.Serialization;
using KademliaSharp.Connection;

namespace KademliaSharp.Node;

public interface INode<out TId, out TC> 
    where TId : INumber<TId>
    where TC : IConnection
{
    TC GetConnectionInfo();
    TId GetId();
}