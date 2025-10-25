using System.Numerics;
using KademliaSharp.Connection;
using KademliaSharp.Node.Strategies;
using KademliaSharp.protocol.handler;
using KademliaSharp.protocol.message;
using KademliaSharp.RoutingTable;
using KademliaSharp.RoutingTable.Bucket;

namespace KademliaSharp.Node.NodeAPI;

public class KademliaNode<TId, TC>(TId id, TC connection) : Node<TId, TC>(id, connection), IKademliaNodeApi<TId, TC>
    where TId : INumber<TId>
    where TC : IConnection
{
    public TC GetConnectionInfo()
    {
        throw new NotImplementedException();
    }

    public TId GetId()
    {
        throw new NotImplementedException();
    }

    public IRoutingTable<TId, TC, IBucket<TId, TC>> GetRoutingTable()
    {
        throw new NotImplementedException();
    }

    public void Start()
    {
        throw new NotImplementedException();
    }

    public Task<bool> StartAsync(Node<TId, TC> bootstrapNode)
    {
        throw new NotImplementedException();
    }

    public void Stop()
    {
        throw new NotImplementedException();
    }

    public void StopImmediate()
    {
        throw new NotImplementedException();
    }

    bool IKademliaNodeApi<TId, TC>.IsRunning()
    {
        throw new NotImplementedException();
    }

    IMessageSender<TId, TC> IKademliaNodeApi<TId, TC>.MessageSender()
    {
        throw new NotImplementedException();
    }
    public KademliaMessage<TId, TC, TData> OnMessage<TData>(KademliaMessage<TId, TC, TData> message) where TData : class
    {
        throw new NotImplementedException();
    }

    public void RegisterMessageHandler(string type, IMessageHandler<TId, TC> handler)
    {
        throw new NotImplementedException();
    }

    public IMessageHandler<TId, TC>? GetHandler(string type)
    {
        throw new NotImplementedException();
    }

    public void SetReferencedNodesStrategy(IReferencedNodesStrategy strategy)
    {
        throw new NotImplementedException();
    }
}