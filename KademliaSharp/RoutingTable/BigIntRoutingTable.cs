using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.RoutingTable.Bucket;

namespace KademliaSharp.RoutingTable;

public class BigIntRoutingTable<TC>(BigInteger localNodeId) : 
    AbstractRoutingTable<BigInteger, TC, BigIntBucket<TC>>(localNodeId) where TC : IConnection
{
    
}