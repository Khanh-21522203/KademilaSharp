using System.Numerics;
using System.Runtime.Serialization;
using KademliaSharp.Connection;
using KademliaSharp.Node;

namespace KademliaSharp.protocol.message;

public abstract class KademliaMessage<TI, TC, TD>
    where TI : INumber<TI>
    where TC : IConnection
    where TD : class
{
    public TD Data { get; set; }
    public string Type { get; set; }
    public INode<TI, TC> Node { get; set; }
    public bool IsAlive { get; set; } = true;

    public override bool Equals(object obj)
    {
        if (this == obj) return true;
        if (GetType() != obj.GetType()) return false;

        var other = (KademliaMessage<TI, TC, TD>)obj;
        
        return EqualityComparer<TD>.Default.Equals(Data, other.Data) &&
               EqualityComparer<string>.Default.Equals(Type, other.Type) &&
               EqualityComparer<INode<TI, TC>>.Default.Equals(Node, other.Node) &&
               IsAlive == other.IsAlive;
    }
    
    public override int GetHashCode()
    {
        var hash = new HashCode();
        hash.Add(Data);
        hash.Add(Type);
        hash.Add(Node);
        return hash.ToHashCode();
    }
}