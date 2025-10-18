using System.Numerics;
using System.Runtime.Serialization;
using KademliaSharp.Node;
using KademliaSharp.Node.NodeAPI;
using KademliaSharp.protocol.message;

namespace KademliaSharp.Connection;

public interface IMessageSender<T, TC> 
    where T : INumber<T>
    where TC : IConnection
{
    KademliaMessage<T, TC, TO> SendMessage<TU, TO>(
        IKademliaNodeApi<T, TC> caller,
        INode<T, TC> receiver,
        KademliaMessage<T, TC, TU> message
    )
        where TU : class
        where TO : class;

    void SendAsyncMessage<TU>(
        IKademliaNodeApi<T, TC> caller,
        INode<T, TC> receiver,
        KademliaMessage<T, TC, TU> message
    )
        where TU : class;
}