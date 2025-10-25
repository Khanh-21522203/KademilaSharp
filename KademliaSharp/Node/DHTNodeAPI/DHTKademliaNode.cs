using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Model;
using KademliaSharp.Node.Decorator;
using KademliaSharp.Node.NodeAPI;
using KademliaSharp.Repository;

namespace KademliaSharp.Node.DHTNodeAPI;

public class DhtKademliaNode<TId, TC, TK, TV>(IKademliaNodeApi<TId, TC> kademliaNode, IKademliaRepository<TK, TV> kademliaRepository, IKeyHashGenerator<TId, TK> keyHashGenerator) 
    : KademliaNodeApiDecorator<TId, TC>(kademliaNode), IDhtKademliaNodeApi<TId, TC, TK, TV>
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

    public IKademliaRepository<TK, TV> KademliaRepository { get; } = kademliaRepository;
    public IKeyHashGenerator<TId, TK> KeyHashGenerator { get; } = keyHashGenerator;
}