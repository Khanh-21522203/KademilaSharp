using KademliaSharp.Protocol.Grpc;
using KademliaSharp.Repository;
using KademliaSharp.Table;

namespace KademliaSharp.Services;

public interface IStoreService
{
    Task<StoreResult> StoreAsync(NodeId key, byte[] value);
    Task<bool> OnStoreAsync(Contact sender, NodeId key, byte[] value);
    bool GetValueAsync(NodeId key, out byte[]? value);
}
public record struct StoreResult(bool Success, int StoredNodeCount, List<Contact> FailedNodes);

public class StoreService: IStoreService
{
    private readonly RoutingTable _routingTable;
    private readonly IKademliaClient _kademliaClient;
    private readonly IKeyValueStore<NodeId, byte[]> _keyValueStore;
    private readonly IPersistentStorage<NodeId, byte[]> _persistentStorage;
    private readonly int _replicationFactor;

    public StoreService(RoutingTable routingTable, IKademliaClient kademliaClient, IKeyValueStore<NodeId, byte[]> keyValueStore,
        int replicationFactor, IPersistentStorage<NodeId, byte[]> persistentStorage)
    {
        _routingTable = routingTable;
        _kademliaClient = kademliaClient;
        _keyValueStore = keyValueStore;
        _replicationFactor = replicationFactor;
        _persistentStorage = persistentStorage;
    }

    public Task<StoreResult> StoreAsync(NodeId key, byte[] value)
    {
        throw new NotImplementedException();
    }

    public Task<bool> OnStoreAsync(Contact sender, NodeId key, byte[] value)
    {
        var storeSuccess = _persistentStorage.SaveAsync(key, value)
            .GetAwaiter().GetResult();
        if (!storeSuccess)
        {
            return Task.FromResult(false);
        }
        _keyValueStore.Set(key, value, TimeSpan.FromHours(1));
        return Task.FromResult(true);
    }

    public bool GetValueAsync(NodeId key, out byte[]? value)
    {
        return _keyValueStore.TryGet(key, out value);
    }
}