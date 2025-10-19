using KademliaSharp.Connection;
using KademliaSharp.RoutingTable.Bucket;

namespace KademliaSharp.RoutingTable;

public class LongRoutingTable<TC>(long localNodeId) : 
    AbstractRoutingTable<long, TC, LongBucket<TC>>(localNodeId) where TC : IConnection
{
    
}