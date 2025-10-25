using System.Numerics;
using System.Runtime.Serialization;
using KademliaSharp.Connection;
using KademliaSharp.Node;

namespace KademliaSharp.Model;

public enum Result {
    Stored,
    Passed,
    Failed,
    Found,
    NotFound
}