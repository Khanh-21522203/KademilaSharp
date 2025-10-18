using System.Numerics;

namespace KademliaSharp.Node;

public interface IKeyHashGenerator<TId, TK>
    where TId : INumber<TId>
{
    TId GenerateKeyHash(TK key);
}