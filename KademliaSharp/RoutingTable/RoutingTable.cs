using System.Numerics;
using KademliaSharp.Connection;

namespace KademliaSharp.RoutingTable;

public interface IRoutingTable<TId, TC, TB>
    where TId : INumber<TId>
    where TC : IConnection
    where TB : IBucket<TId, TC>
{
    
}