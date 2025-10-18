using KademliaSharp.Connection;

namespace KademliaSharp.Node.RoutedNode;

public class LongRoutedNode<TC>(long distance) : RoutedNode<long, TC>(distance)
    where TC : IConnection
{
    public override int CompareTo(RoutedNode<long, TC> other)
    {
        return Distance.CompareTo(other.Distance);
    }
}