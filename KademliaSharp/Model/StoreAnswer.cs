using System.Numerics;
using System.Runtime.Serialization;
using KademliaSharp.Connection;

namespace KademliaSharp.Model;

public class StoreAnswer<TId, TC, TK>: Answer<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    private TK _key;
    private Result _result = Result.Failed;
}