using Grpc.Net.Client;
using KademliaSharp.Protocol.Grpc.Messages;
using MagicOnion.Client;

namespace KademliaSharp.Protocol.Grpc;

public interface IKademliaClient
{
    Task<PongMessage> Ping(PingMessage request);
    Task<ShutdownMessage> Shutdown(ShutdownMessage request);
    Task<FindNodeResponse> FindNode(FindNodeRequest request);
    Task<DhtLookupResponse> DhtLookup(DhtLookupRequest request);
    Task<DhtStoreResponse> DhtStore(DhtStoreRequest request);
}
public class KademliaClient: IKademliaClient
{
    private async Task<TResponse> ExecuteRequest<TRequest, TResponse>(
        Func<IKademliaService, TRequest, Task<TResponse>> func,
        TRequest request,
        string host,
        int port)
    {
        var grpcChannel = GrpcChannel.ForAddress($"http://{host}:{port}");
        var client = MagicOnionClient.Create<IKademliaService>(grpcChannel);
        try
        {
            var response = await func(client, request);
            return response;
        }
        catch (Exception)
        {
            throw;
        }
        finally
        {
            await grpcChannel.ShutdownAsync();
        }
    }
    
    public async Task<PongMessage> Ping(PingMessage request)
    {
        return await ExecuteRequest<PingMessage, PongMessage>(
            (client, req) => client.Ping(req),
            request,
            request.TargetHost,
            request.TargetPort);
    }

    public async Task<ShutdownMessage> Shutdown(ShutdownMessage request)
    {
        return await ExecuteRequest<ShutdownMessage, ShutdownMessage>(
            (client, req) => client.Shutdown(req),
            request,
            request.TargetHost,
            request.TargetPort);
    }

    public async Task<FindNodeResponse> FindNode(FindNodeRequest request)
    {
        return await ExecuteRequest<FindNodeRequest, FindNodeResponse>(
            (client, req) => client.FindNode(req),
            request,
            request.TargetHost,
            request.TargetPort);
    }

    public async Task<DhtLookupResponse> DhtLookup(DhtLookupRequest request)
    {
        return await ExecuteRequest<DhtLookupRequest, DhtLookupResponse>(
            (client, req) => client.DhtLookup(req),
            request,
            request.TargetHost,
            request.TargetPort);
    }

    public async Task<DhtStoreResponse> DhtStore(DhtStoreRequest request)
    {
        return await ExecuteRequest<DhtStoreRequest, DhtStoreResponse>(
            (client, req) => client.DhtStore(req),
            request,
            request.TargetHost,
            request.TargetPort);
    }
}