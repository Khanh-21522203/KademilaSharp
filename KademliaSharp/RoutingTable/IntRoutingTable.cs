using KademliaSharp.Connection;
using KademliaSharp.RoutingTable.Bucket;

namespace KademliaSharp.RoutingTable;

public class IntRoutingTable<TC>(int localNodeId) : 
    AbstractRoutingTable<int, TC, IntBucket<TC>>(localNodeId) where TC : IConnection
{
    
}