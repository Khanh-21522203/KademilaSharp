using System.Numerics;

namespace KademliaSharp.Node.RoutedNode;

public class BigIntegerRoutedNode(BigInteger id, BigInteger distance)
    : RoutedNode<BigInteger>(id, distance)
{
    protected override int CompareTo(RoutedNode<BigInteger> other)
    {
        return Distance.CompareTo(other.Distance);
    }
}