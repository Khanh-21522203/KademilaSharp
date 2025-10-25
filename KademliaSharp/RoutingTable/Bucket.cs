using KademliaSharp.Utils.Collections;

namespace KademliaSharp.RoutingTable;

public interface IBucket
{
    void AddContact(Contact contact);
    bool RemoveContact(NodeId nodeId);
    void UpdateContact(NodeId nodeId);
    IReadOnlyCollection<Contact> GetContacts();
    IReadOnlyCollection<Contact> GetNearestNode(NodeId target, int count);
    bool IsFull { get; }
    int Count { get; }
    DateTime LastUpdated { get; }
}
public class KBucket(int capacity) : IBucket
{
    private readonly ConcurrentLruCache<NodeId, Contact> _contacts = new(capacity);
    private readonly int _capacity = capacity;

    public void AddContact(Contact contact)
    {
        _contacts.Put(contact.NodeId, contact);
        LastUpdated = DateTime.UtcNow;
    }

    public bool RemoveContact(NodeId nodeId)
    {
        return _contacts.Remove(nodeId);
    }

    public void UpdateContact(NodeId nodeId)
    {
        _contacts.Update(nodeId);
        LastUpdated = DateTime.UtcNow;
    }

    public IReadOnlyCollection<Contact> GetContacts()
    {
        return _contacts.GetValues();
    }
    
    public IReadOnlyCollection<Contact> GetNearestNode(NodeId target, int count)
    {
        var sortedContacts = _contacts.GetValues()
            .OrderBy(c => c.NodeId.XorDistance(target))
            .Take(count)
            .ToList();
        return sortedContacts;
    }

    public bool IsFull => _contacts.Count >= _capacity;
    public int Count => _contacts.Count;
    public DateTime LastUpdated { get; private set; } = DateTime.UtcNow;
}