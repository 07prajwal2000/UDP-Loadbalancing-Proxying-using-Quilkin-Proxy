using LiteNetLib.Layers;
using System.Net;
using System.Text;

namespace RUDPClient;

internal class BasePacketLayer : PacketLayerBase
{
    private byte[] serverKey;
    public BasePacketLayer() : base(4)
    {
        serverKey = Encoding.UTF8.GetBytes("s001");
        Console.WriteLine("Base64 key: " + Convert.ToBase64String(serverKey));
    }

    public override void ProcessInboundPacket(ref IPEndPoint endPoint, ref byte[] data, ref int length)
    {
        
    }

    public override void ProcessOutBoundPacket(ref IPEndPoint endPoint, ref byte[] data, ref int offset, ref int length)
    {
        var newData = new byte[data.Length];
        Buffer.BlockCopy(data, 0, newData, 4, data.Length - 4);
        Buffer.BlockCopy(serverKey, 0, newData, 0, 4);
        data = newData;
        length += 4;
    }
}
