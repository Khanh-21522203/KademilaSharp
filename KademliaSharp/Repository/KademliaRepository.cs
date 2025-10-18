namespace KademliaSharp.Repository;

public interface IKademliaRepository<TK, TV>
{
    bool Store(TK key, TV value);
    bool TryGetValue(TK key, out TV value);
    bool TryRemove(TK key, out TV value);
}