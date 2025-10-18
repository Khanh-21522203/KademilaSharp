using System.Numerics;
using KademliaSharp.Connection;

namespace KademliaSharp.Model;

public class FindNodeAnswer<TId, TC>: Answer<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    
}