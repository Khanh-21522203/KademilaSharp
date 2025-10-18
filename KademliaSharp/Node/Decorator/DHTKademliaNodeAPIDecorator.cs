using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Model;
using KademliaSharp.Node.DHTNodeAPI;
using KademliaSharp.Repository;

namespace KademliaSharp.Node.Decorator;

public class DhtKademliaNodeApiDecorator<TId, TC, TK, TV>(IDhtKademliaNodeApi<TId, TC, TK, TV> dhtKademliaNode) 
    : KademliaNodeApiDecorator<TId, TC>(dhtKademliaNode), IDhtKademliaNodeApi<TId, TC, TK, TV> 
    where TId : INumber<TId>
    where TC : IConnection
{
    public Task<StoreAnswer<TId, TC, TK>> StoreAsync(TK key, TV value)
    {
        return dhtKademliaNode.StoreAsync(key, value);
    }

    public Task<LookupAnswer<TId, TC, TK, TV>> LookupAsync(TK key)
    {
        return dhtKademliaNode.LookupAsync(key);
    }

    public IKademliaRepository<TK, TV> KademliaRepository => dhtKademliaNode.KademliaRepository;
    public IKeyHashGenerator<TId, TK> KeyHashGenerator => dhtKademliaNode.KeyHashGenerator;
}