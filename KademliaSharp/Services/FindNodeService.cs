using KademliaSharp.Table;

namespace KademliaSharp.Services;

public interface IFindNodeService
{
    Task<List<Contact>> OnFindNodeAsync(Contact sender, NodeId targetNodeId);
}
public class FindNodeService(RoutingTable routingTable, int maxResponseSize = 20) : IFindNodeService
{
    private readonly RoutingTable _routingTable = routingTable;
    private readonly int _maxResponseSize = maxResponseSize;

    public Task<List<Contact>> OnFindNodeAsync(Contact sender, NodeId targetNodeId)
    {
        throw new NotImplementedException();
    }
    
    private void UpdateRoutingTable(Contact sender)
    {
        _routingTable.UpdateNode(sender.NodeId);
    }
}