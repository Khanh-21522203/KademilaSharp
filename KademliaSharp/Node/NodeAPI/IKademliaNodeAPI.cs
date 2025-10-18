using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Node.Strategies;
using KademliaSharp.protocol.handler;
using KademliaSharp.protocol.message;
using KademliaSharp.RoutingTable;

namespace KademliaSharp.Node.NodeAPI;

public interface IKademliaNodeApi<TId, TC>: INode<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    IRoutingTable<TId, TC, IBucket<TId, TC>> GetRoutingTable();
    void Start();
    Task<bool> StartAsync(INode<TId, TC> bootstrapNode);
    void Stop();
    void StopImmediate();
    bool IsRunning { get; }
    IMessageSender<TId, TC> MessageSender { get; }

    // NodeSettings NodeSettings { get; }

    KademliaMessage<TId, TC, TData> OnMessage<TData>(KademliaMessage<TId, TC, TData> message)
        where TData : class;

    void RegisterMessageHandler(string type, IMessageHandler<TId, TC> handler);

    IMessageHandler<TId, TC>? GetHandler(string type);

    void SetReferencedNodesStrategy(IReferencedNodesStrategy strategy);
}