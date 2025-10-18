using System.Numerics;
using KademliaSharp.Connection;

namespace KademliaSharp.Node.Decorator;

public class NodeDecorator<TId, TC>(INode<TId, TC> node) : INode<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    protected readonly INode<TId, TC> Node = node;

    public TC GetConnectionInfo() => Node.GetConnectionInfo();

    public TId GetId() => Node.GetId();
}