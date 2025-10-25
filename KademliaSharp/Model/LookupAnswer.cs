using System.Numerics;
using KademliaSharp.Connection;

namespace KademliaSharp.Model;

public record LookupAnswer<TId, TC, TK, TV>(TK Key, TV Value, Result Result)
    where TId : INumber<TId>
    where TC : IConnection
{ }