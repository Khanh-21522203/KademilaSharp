using MessagePack;

namespace KademliaSharp.Protocol.Grpc.Messages;

[MessagePackObject]
public record struct PingMessage(
    [property: Key(0)] string SenderNodeId,
    [property: Key(1)] string TargetHost,
    [property: Key(2)] int TargetPort
);

[MessagePackObject]
public record struct PongMessage(
    [property: Key(0)] string SenderNodeId
);

[MessagePackObject]
public record struct ShutdownMessage(
    [property: Key(0)] string SenderNodeId,
    [property: Key(1)] string TargetHost,
    [property: Key(2)] int TargetPort
);

[MessagePackObject]
public record struct FindNodeRequest(
    [property: Key(0)] string SenderNodeId,
    [property: Key(1)] string TargetNodeId,
    [property: Key(2)] string TargetHost,
    [property: Key(3)] int TargetPort
);

[MessagePackObject]
public record struct FindNodeResponse(
    [property: Key(0)] string SenderNodeId,
    [property: Key(1)] List<string> ClosestNodeIds
);

public enum RequestResult
{
    Success,
    Failed
}

[MessagePackObject]
public record struct DhtLookupRequest(
    [property: Key(0)] string SenderNodeId,
    [property: Key(1)] string SourceNodeId,
    [property: Key(1)] byte[] Key,
    [property: Key(2)] string TargetHost,
    [property: Key(3)] int TargetPort
);

[MessagePackObject]
public record struct DhtLookupResponse(
    [property: Key(0)] string SenderNodeId,
    [property: Key(1)] RequestResult Result,
    [property: Key(2)] byte[] Key,
    [property: Key(3)] byte[] Value
);

[MessagePackObject]
public record struct DhtStoreRequest(
    [property: Key(0)] string SenderNodeId,
    [property: Key(1)] byte[] Key,
    [property: Key(2)] byte[] Value,
    [property: Key(3)] string TargetHost,
    [property: Key(4)] int TargetPort
);

[MessagePackObject]
public record struct DhtStoreResponse(
    [property: Key(0)] string SenderNodeId,
    [property: Key(1)] RequestResult Result,
    [property: Key(2)] byte[] Key
);