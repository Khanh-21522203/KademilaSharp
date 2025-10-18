using KademliaSharp.Connection;

namespace KademliaSharp.Node;

public abstract class ExternalNode<TId, TConnection> : DateAwareNodeDecorator<TId, TConnection>, IComparable<object>
    where TId : struct, IComparable<TId>, IEquatable<TId>
    where TConnection : IConnection
{
    protected TId distance;

    protected ExternalNode(INode<TId, TConnection> node, TId distance) : base(node)
    {
        this.distance = distance;
    }

    public abstract int CompareTo(object obj);

    public override bool Equals(object obj)
    {
        if (this == obj) return true;
        if (obj == null || GetType() != obj.GetType()) return false;
        
        ExternalNode<TId, TConnection> other = (ExternalNode<TId, TConnection>)obj;
        return EqualityComparer<TId>.Default.Equals(distance, other.distance) && 
               EqualityComparer<INode<TId, TConnection>>.Default.Equals(this.node, other.node);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(distance, GetLastSeen());
    }

    public override string ToString()
    {
        return $"ExternalNode [id={GetId()}, distance={distance}]";
    }
}