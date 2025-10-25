using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Node.NodeAPI;
using KademliaSharp.Node.Strategies;
using KademliaSharp.protocol.handler;
using KademliaSharp.protocol.message;
using KademliaSharp.RoutingTable;
using KademliaSharp.RoutingTable.Bucket;

namespace KademliaSharp.Node.Decorator;

public abstract class KademliaNodeApiDecorator<TId, TC> (IKademliaNodeApi<TId, TC> kademliaNode): IKademliaNodeApi<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    private readonly IKademliaNodeApi<TId, TC> _kademliaNode = kademliaNode;

    public IRoutingTable<TId, TC, IBucket<TId, TC>> GetRoutingTable() => _kademliaNode.GetRoutingTable();

    public void Start() => _kademliaNode.Start();

    public Task<bool> StartAsync(Node<TId, TC> bootstrapNode) => _kademliaNode.StartAsync(bootstrapNode);

    public void Stop() => _kademliaNode.Stop();

    public void StopImmediate() => _kademliaNode.StopImmediate();

    public bool IsRunning() => _kademliaNode.IsRunning();
    
    public IMessageSender<TId, TC> MessageSender() => _kademliaNode.MessageSender();
    public KademliaMessage<TId, TC, TData> OnMessage<TData>(KademliaMessage<TId, TC, TData> message) where TData : class
        => _kademliaNode.OnMessage(message);

    public void RegisterMessageHandler(string type, IMessageHandler<TId, TC> handler)
        => _kademliaNode.RegisterMessageHandler(type, handler);

    public IMessageHandler<TId, TC>? GetHandler(string type)
        => _kademliaNode.GetHandler(type);

    public void SetReferencedNodesStrategy(IReferencedNodesStrategy strategy)
        => _kademliaNode.SetReferencedNodesStrategy(strategy);
}