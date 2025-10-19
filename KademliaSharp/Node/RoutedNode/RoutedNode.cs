using System.Numerics;
using KademliaSharp.Connection;

namespace KademliaSharp.Node.RoutedNode;

public abstract class RoutedNode<TI, TC>(TI distance): IComparable, INode<TI, TC>
    where TI : INumber<TI>
    where TC : IConnection
{
    public readonly TI Distance = distance;
    
    public int CompareTo(object? obj)
    {
        return obj switch
        {
            null => 1,
            RoutedNode<TI, TC> other => CompareTo(other),
            _ => throw new ArgumentException($"Object must be of type {nameof(RoutedNode<TI, TC>)}")
        };
    } 
    public abstract int CompareTo(RoutedNode<TI, TC> other);
    public TC GetConnectionInfo()
    {
        throw new NotImplementedException();
    }

    public TI GetId()
    {
        throw new NotImplementedException();
    }
}