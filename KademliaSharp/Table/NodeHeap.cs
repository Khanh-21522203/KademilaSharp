namespace KademliaSharp.Table;

public class NodeHeap(NodeId target)
{
    private readonly PriorityQueue<Contact, NodeId> _heap = new();
    private readonly NodeId _target = target;
    private readonly HashSet<NodeId> _visited = new();

    public void Push(Contact contacts)
    {
        if (_visited.Add(contacts.NodeId))
        {
            var priority = contacts.NodeId.XorDistance(_target);
            _heap.Enqueue(contacts, priority);
        }
    }
    
    public bool TryPop(out Contact? contact)
    {
        if (_heap.Count > 0)
        {
            contact = _heap.Dequeue();
            return true;
        }
        contact = null!;
        return false;
    }

    public bool TryPeek(out Contact? contact)
    {
        if (_heap.Count > 0)
        {
            contact = _heap.Peek();
            return true;
        }
        contact = null!;
        return false;
    }
    
    public bool Contains(NodeId nodeId)
    {
        return _visited.Contains(nodeId);
    }
    
    public int Count => _heap.Count;
}