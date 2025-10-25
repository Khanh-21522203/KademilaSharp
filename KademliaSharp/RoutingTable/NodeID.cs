using System.Numerics;

namespace KademliaSharp.RoutingTable;

public readonly record struct NodeId(byte[] Id)
{
    public int CompareTo(NodeId other)
    {
        if (Id.Length != other.Id.Length)
            throw new ArgumentException("Node IDs must have the same length");

        for (var i = 0; i < Id.Length; i++)
        {
            if (Id[i] != other.Id[i])
                return Id[i].CompareTo(other.Id[i]);
        }

        return 0;
    }
    
    public NodeId XorDistance(NodeId other)
    {
        if (Id.Length != other.Id.Length)
            throw new ArgumentException("Node IDs must have the same length");

        var result = new byte[Id.Length];
        for (var i = 0; i < Id.Length; i++)
        {
            result[i] = (byte)(Id[i] ^ other.Id[i]);
        }

        return new NodeId(result);
    }
    
    public int LeadingZeros() 
    {
        var zeros = 0;
    
        foreach (var b in Id) 
        {
            if (b == 0) zeros += 8;
            else 
            {
                zeros += BitOperations.LeadingZeroCount(b) - 24;
                break;
            }
        }
    
        return zeros;
    }
    
    public static NodeId FromBytes(byte[] bytes)
    {
        return new NodeId(bytes);
    }
    
    public static NodeId FromHexString(string hex)
    {
        if (hex.Length % 2 != 0)
            throw new ArgumentException("Hex string must have an even length");

        var bytes = new byte[hex.Length / 2];
        for (var i = 0; i < bytes.Length; i++)
        {
            bytes[i] = Convert.ToByte(hex.Substring(i * 2, 2), 16);
        }

        return new NodeId(bytes);
    }
    
    public static NodeId GenerateRandom(int length)
    {
        var bytes = new byte[length];
        var rng = new Random();
        rng.NextBytes(bytes);
        return new NodeId(bytes);
    }
}