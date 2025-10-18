using KademliaSharp.Connection;

namespace KademliaSharp.Node.RoutedNode;

public class IntegerRoutedNode<TC>(int distance) : RoutedNode<int, TC>(distance)
    where TC : IConnection
{
    public override int CompareTo(RoutedNode<int, TC> other)
    {
        return Distance.CompareTo(other.Distance);
    }
    
}