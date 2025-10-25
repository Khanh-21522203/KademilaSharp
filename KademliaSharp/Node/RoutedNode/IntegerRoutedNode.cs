
namespace KademliaSharp.Node.RoutedNode;

public class IntegerRoutedNode<TC>(int id, int distance)
    : RoutedNode<int>(id, distance)
{
    protected override int CompareTo(RoutedNode<int> other)
    {
        return Distance.CompareTo(other.Distance);
    }
    
}