namespace KademliaSharp.Table;

public class RoutingTable
{
    private readonly List<IBucket> _buckets;

    public RoutingTable(NodeId localNodeId, int maxContractPerBucket = 20)
    {
        _buckets = new List<IBucket>();
        for (var i = 0; i < localNodeId.Id.Length * 8; i++)
        {
            _buckets.Add(new KBucket(maxContractPerBucket));
        }
        LocalNodeId = localNodeId;
    }

    public NodeId LocalNodeId { get; }

    public void AddNode(Contact contact)
    {
        var bucketIndex = GetBucketIndex(contact.NodeId);
        var bucket = _buckets[bucketIndex];
        bucket.AddContact(contact);
    }
    
    public void RemoveNode(NodeId nodeId)
    {
        var bucketIndex = GetBucketIndex(nodeId);
        var bucket = _buckets[bucketIndex];
        bucket.RemoveContact(nodeId);
    }
    
    public void UpdateNode(NodeId nodeId)
    {
        var bucketIndex = GetBucketIndex(nodeId);
        var bucket = _buckets[bucketIndex];
        bucket.UpdateContact(nodeId);
    }
    
    public List<Contact> FindClosestNodes(NodeId target, int count)
    {
        var allContacts = new List<Contact>();
        
        // Collect contacts from all buckets
        foreach (var bucket in _buckets)
        {
            allContacts.AddRange(bucket.GetContacts());
        }
        
        // Sort by XOR distance and take the closest K nodes
        return allContacts
            .OrderBy(c => c.NodeId.XorDistance(target))
            .Take(count)
            .ToList();
    }
    
    public int GetBucketIndex(NodeId nodeId)
    {
        var distance = LocalNodeId.XorDistance(nodeId);
        var leadingZeros = distance.LeadingZeros();
        return leadingZeros;
    }
    
    public IBucket GetBucket(int index)
    {
        return _buckets[index];
    }

    public IEnumerable<int> GetBucketsRequiringRefresh(TimeSpan refreshInterval)
    {
        return [];
    }
}