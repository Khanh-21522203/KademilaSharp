using System.Numerics;
using KademliaSharp.Connection;

namespace KademliaSharp.Node.Decorator;

public class DateAwareNodeDecorator<TId, TC>(Node<TId, TC> node) : NodeDecorator<TId, TC>(node)
    where TId : INumber<TId>
    where TC : IConnection
{
    private DateTime _date = new DateTime();
    
    public void SetDate(DateTime date) => _date = date;
    public DateTime GetDate() => _date;
}