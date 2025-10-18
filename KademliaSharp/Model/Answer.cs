using System.Numerics;
using System.Runtime.Serialization;
using KademliaSharp.Connection;
using KademliaSharp.Node;

namespace KademliaSharp.Model;

[Serializable]
public class Answer<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    private INode<TId, TC> _node;
    private bool _isAlive;
}

public enum Result {
    Stored,
    Passed,
    Failed
}