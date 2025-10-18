using System.Numerics;
using System.Runtime.Serialization;
using KademliaSharp.Connection;
using KademliaSharp.Node;
using KademliaSharp.Node.NodeAPI;
using KademliaSharp.protocol.message;

namespace KademliaSharp.protocol.handler;

public interface IMessageHandler<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    KademliaMessage<TId, TC, TO> Handle<TU, TO>(IKademliaNodeApi<TId, TC> kademliaNode, KademliaMessage<TId, TC, TU> message)
        where TU : class
        where TO : class;
}