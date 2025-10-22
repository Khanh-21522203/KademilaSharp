using KademliaSharp.Protocol.Grpc.Messages;
using MagicOnion;
using MagicOnion.Server;
using MagicOnion.Server.Hubs;

namespace KademliaSharp.Protocol.Grpc;

public interface IKademliaService : IService<IKademliaService>
{
    Task<PongMessage> Ping(PingMessage request);
    Task<ShutdownMessage> Shutdown(ShutdownMessage request);
    Task<FindNodeResponse> FindNode(FindNodeRequest request);
    Task<DhtLookupResponse> DhtLookup(DhtLookupRequest request);
    Task<DhtStoreResponse> DhtStore(DhtStoreRequest request);
}

public abstract class KademliaService : ServiceBase<IKademliaService>, IKademliaService
{
    public async Task<PongMessage> Ping(PingMessage request)
    {
        throw new NotImplementedException();
    }

    public async Task<ShutdownMessage> Shutdown(ShutdownMessage request)
    {
        throw new NotImplementedException();
    }

    public async Task<FindNodeResponse> FindNode(FindNodeRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<DhtLookupResponse> DhtLookup(DhtLookupRequest request)
    {
        throw new NotImplementedException();
    }

    public async Task<DhtStoreResponse> DhtStore(DhtStoreRequest request)
    {
        throw new NotImplementedException();
    }
}