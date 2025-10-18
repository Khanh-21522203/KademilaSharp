using System.Numerics;
using KademliaSharp.Connection;

namespace KademliaSharp.Model;

public class LookupAnswer<TId, TC, TK, TV>: Answer<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    private TK _key;
    private TV _value;
    private Result _result = Result.Failed;
}