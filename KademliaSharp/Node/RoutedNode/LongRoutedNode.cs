namespace KademliaSharp.Node.RoutedNode;

public class LongRoutedNode(long id, long distance)
    : RoutedNode<long>(id, distance)
{
    protected override int CompareTo(RoutedNode<long> other)
    {
        return Distance.CompareTo(other.Distance);
    }
}