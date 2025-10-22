using System.Text;
using MessagePack;

namespace KademliaSharp.Protocol.Grpc.Messages;

/// <summary>
/// Adapter for converting between internal Kademlia messages and gRPC DTOs
/// </summary>
public static class MessageAdapter
{
    /// <summary>
    /// Convert byte[] key to string representation
    /// </summary>
    public static byte[] SerializeKey<TK>(TK key) where TK : notnull
    {
        if (key is byte[] bytes)
            return bytes;
        
        if (key is string str)
            return Encoding.UTF8.GetBytes(str);
        
        // Use MessagePack for complex types
        return MessagePackSerializer.Serialize(key);
    }

    /// <summary>
    /// Deserialize key from byte[]
    /// </summary>
    public static TK DeserializeKey<TK>(byte[] keyBytes)
    {
        if (typeof(TK) == typeof(byte[]))
            return (TK)(object)keyBytes;
        
        if (typeof(TK) == typeof(string))
            return (TK)(object)Encoding.UTF8.GetString(keyBytes);
        
        return MessagePackSerializer.Deserialize<TK>(keyBytes);
    }

    /// <summary>
    /// Serialize value to byte[]
    /// </summary>
    public static byte[] SerializeValue<TV>(TV value)
    {
        if (value is byte[] bytes)
            return bytes;
        
        if (value is string str)
            return Encoding.UTF8.GetBytes(str);
        
        return MessagePackSerializer.Serialize(value);
    }

    /// <summary>
    /// Deserialize value from byte[]
    /// </summary>
    public static TV DeserializeValue<TV>(byte[] valueBytes)
    {
        if (typeof(TV) == typeof(byte[]))
            return (TV)(object)valueBytes;
        
        if (typeof(TV) == typeof(string))
            return (TV)(object)Encoding.UTF8.GetString(valueBytes);
        
        return MessagePackSerializer.Deserialize<TV>(valueBytes);
    }
}
