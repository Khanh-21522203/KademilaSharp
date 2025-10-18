using System.Numerics;
using KademliaSharp.Connection;

namespace KademliaSharp.Node.RoutedNode;

public class BigIntegerRoutedNode<TC>(BigInteger distance) : RoutedNode<BigInteger, TC>(distance)
    where TC : IConnection
{
    public override int CompareTo(RoutedNode<BigInteger, TC> other)
    {
        return Distance.CompareTo(other.Distance);
    }
}