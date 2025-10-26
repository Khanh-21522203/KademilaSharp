using KademliaSharp.Protocol.Grpc;
using KademliaSharp.Table;
using Serilog;

namespace KademliaSharp.Services;

public interface ILookupService
{
    Task<List<Contact>> FindNodeAsync(NodeId targetNodeId, int count);
    Task<List<LookupResult>> FindValueAsync(NodeId key);
}

public record struct LookupResult(bool Found, List<Contact> Contacts, byte[]? Value = null)
{
}

public class LookupService : ILookupService
{
    private readonly RoutingTable _routingTable;
    private readonly IKademliaClient _kademliaClient;
    private readonly int _maxParallelQueries;
    private readonly int _maxResponseSize;
    private readonly ILogger _logger;

    public LookupService(RoutingTable routingTable, IKademliaClient kademliaClient, int maxParallelQueries, int maxResponseSize, ILogger logger)
    {
        _routingTable = routingTable;
        _kademliaClient = kademliaClient;
        _maxParallelQueries = maxParallelQueries;
        _maxResponseSize = maxResponseSize;
        _logger = logger;
    }

    public async Task<List<Contact>> FindNodeAsync(NodeId targetNodeId, int count)
    {
        var result = await PerformIterativeLookupAsync(targetNodeId);
        return result.Contacts.Take(count).ToList();
    }

    public Task<List<LookupResult>> FindValueAsync(NodeId key)
    {
        throw new NotImplementedException();
    }

    private async Task<LookupResult> PerformIterativeLookupAsync(NodeId target)
    {
        var shortlist = new NodeHeap(target);
        var queried = new HashSet<NodeId>();
        var initialContacts = _routingTable.FindClosestNodes(target, _maxParallelQueries);
        
        foreach (var contact in initialContacts)
        {
            shortlist.Push(contact);
        }
        
        var roundsWithoutProgress = 0;
        Contact closestSeen = shortlist.TryPeek(out var tmp) ? tmp : initialContacts.FirstOrDefault();
        
        var maxIterations = 16;
        var iter = 0;

        while (true)
        {
            iter++;
            if (iter > maxIterations) break;
            _logger.Information("Lookup iteration {Iteration} with shortList: {shortList}", iter, shortlist);
            
            // pick up to alpha contacts (pop directly from heap)
            var toQuery = SelectNextContacts(shortlist, queried);
            if (toQuery.Count == 0)
            {
                _logger.Information("No more contacts to query, ending lookup.");
                break;
            }
            
            foreach (var contact in toQuery)
                queried.Add(contact.NodeId);
            
            var lookupTask = new List<Task<LookupResult>>(toQuery.Count);
            foreach (var contact in toQuery)
            {
                if (false)
                {
                    lookupTask.Add(QueryForValueAsync());
                }
                else
                {
                    lookupTask.Add(QueryForNodeAsync());
                }
            }

            LookupResult[] responses;
            try
            {
                responses = await Task.WhenAll(lookupTask);
            }
            catch (Exception ex)
            {
                _logger.Error("Error during lookup queries: {Error}", ex.Message);
                continue;
            }

            // Merge replies: for FindNode they supply contacts; for FindValue they may carry value.
            foreach (var response in responses)
            {
                if (response.Found && response.Value != null)
                {
                    _logger.Information("Value found during lookup for target {Target}", target);
                    return response;
                }

                foreach (var contact in response.Contacts)
                {
                    if (contact.NodeId.Equals(default(NodeId)))
                        continue;
                    shortlist.Push(contact);
                }
            }
            
            // Progress detection: compare current closest with previous
            Contact? newClosest = shortlist.TryPeek(out var peek) ? peek : null;
            if (newClosest != null)
            {
                var prevDistance = closestSeen.NodeId.XorDistance(target);
                var newDistance = newClosest.Value.NodeId.XorDistance(target);
                if (newDistance.CompareTo(prevDistance) < 0)
                {
                    closestSeen = newClosest.Value;
                    roundsWithoutProgress = 0;
                    _logger.Information("Progress made in lookup, new closest node: {Closest}", closestSeen);
                }
                else
                {
                    roundsWithoutProgress++;
                    _logger.Information("No progress in this round. Rounds without progress: {Rounds}", roundsWithoutProgress);
                }
            }
            
            if (roundsWithoutProgress > 1)
            {
                _logger.Information("No progress for more than one consecutive rounds, ending lookup.");
                break;
            }
        }
        var closest = SelectNextContacts(shortlist, queried);
        return new LookupResult(false, closest);
    }
    
    private List<Contact> SelectNextContacts(NodeHeap shortlist, HashSet<NodeId> queried)
    {
        var contacts = new List<Contact>();
        
        while (contacts.Count < _maxParallelQueries && shortlist.Count > 0) 
        {
            if (shortlist.TryPop(out var contact))
            {
                contacts.Add(contact);
            }
        }
        
        return contacts;
    }

    private async Task<LookupResult> QueryForValueAsync()
    {
        throw new NotImplementedException();
    }

    private async Task<LookupResult> QueryForNodeAsync()
    {
        throw new NotImplementedException();
    }
}