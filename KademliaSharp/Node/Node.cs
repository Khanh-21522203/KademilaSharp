using System.Numerics;

namespace KademliaSharp.Node;

public abstract class Node<TId>(TId id) 
    where TId : INumber<TId>
{
    public TId Id { get; } = id;
}