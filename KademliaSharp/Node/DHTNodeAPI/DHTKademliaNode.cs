using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Model;
using KademliaSharp.Node.Decorator;
using KademliaSharp.Repository;

namespace KademliaSharp.Node.DHTNodeAPI;

public class DhtKademliaNode<TId, TC, TK, TV> : KademliaNodeApiDecorator<TId, TC>, IDhtKademliaNodeApi<TId, TC, TK, TV>
    where TId : INumber<TId>
    where TC : IConnection
{
    public Task<StoreAnswer<TId, TC, TK>> StoreAsync(TK key, TV value)
    {
        throw new NotImplementedException();
    }

    public Task<LookupAnswer<TId, TC, TK, TV>> LookupAsync(TK key)
    {
        throw new NotImplementedException();
    }

    public IKademliaRepository<TK, TV> KademliaRepository { get; }
    public IKeyHashGenerator<TId, TK> KeyHashGenerator { get; }
}