using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Model;
using KademliaSharp.Node.NodeAPI;
using KademliaSharp.Repository;

namespace KademliaSharp.Node.DHTNodeAPI;

public interface IDhtKademliaNodeApi<TId, TC, TK, TV>: IKademliaNodeApi<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    Task<StoreAnswer<TId, TC, TK>> StoreAsync(TK key, TV value);

    Task<LookupAnswer<TId, TC, TK, TV>> LookupAsync(TK key);

    IKademliaRepository<TK, TV> KademliaRepository { get; }

    IKeyHashGenerator<TId, TK> KeyHashGenerator { get; }
}