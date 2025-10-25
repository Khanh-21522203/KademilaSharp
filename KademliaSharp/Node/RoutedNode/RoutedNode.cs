using System.Numerics;

namespace KademliaSharp.Node.RoutedNode;

public abstract class RoutedNode<TI>(TI id, TI distance)
    : Node<TI>(id), IComparable
    where TI : INumber<TI>
{
    public readonly TI Distance = distance;
    
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            RoutedNode<TI> other => CompareTo(other),
            _ => throw new ArgumentException($"Object must be of type {nameof(RoutedNode<TI>)}")
        };
    }
    protected abstract int CompareTo(RoutedNode<TI> other);
}