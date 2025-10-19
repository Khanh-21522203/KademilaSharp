using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.RoutingTable.Bucket;

namespace KademliaSharp.RoutingTable;

public class RoutingTableFactory<TId, TC, TB>
    where TId : INumber<TId>
    where TC : IConnection
    where TB : IBucket<TId, TC>
{
    public IRoutingTable<TId, TC, TB> CreateRoutingTable(TId localNodeId)
    {
        switch (localNodeId)
        {
            case BigInteger id:
                return (IRoutingTable<TId, TC, TB>)new AbstractRoutingTable<BigInteger, TC, BigIntBucket<TC>>(id);
            case long id:
                return (IRoutingTable<TId, TC, TB>)new AbstractRoutingTable<long, TC, LongBucket<TC>>(id);
            case int id:
                return (IRoutingTable<TId, TC, TB>)new AbstractRoutingTable<int, TC, IntBucket<TC>>(id);
            default:
                throw new NotSupportedException($"The type {typeof(TId)} is not supported for RoutingTable.");
        }
    }
}