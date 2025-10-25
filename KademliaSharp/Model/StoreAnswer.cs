using System.Numerics;
using System.Runtime.Serialization;
using KademliaSharp.Connection;

namespace KademliaSharp.Model;

public class StoreAnswer<TId, TC, TK>(TK key, Result result)
    where TId : INumber<TId>
    where TC : IConnection
{ }