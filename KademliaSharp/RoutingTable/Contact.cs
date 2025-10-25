using System.Net;

namespace KademliaSharp.RoutingTable;

public record struct Contact(NodeId NodeId, IPEndPoint EndPoint)
{
    public DateTime? LastSeen { get; set; } = null;
    public TimeSpan? Rtt { get; set; } = null;

    private int FailedPingCount { get; set; } = 0;

    public void IncrementFailedPing() => FailedPingCount++;
    public void ResetFailedPing() => FailedPingCount = 0;
}