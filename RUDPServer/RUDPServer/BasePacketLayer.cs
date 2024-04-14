using LiteNetLib.Layers;
using System.Net;

namespace RUDPServer;

internal class BasePacketLayer : PacketLayerBase
{
    private bool trimServerToken = true;
    public BasePacketLayer() : base(0)
    {
        trimServerToken = (Environment.GetEnvironmentVariable("TRIM_TOKEN") ?? "false") == "true";
    }

    public override void ProcessInboundPacket(ref IPEndPoint endPoint, ref byte[] data, ref int length)
    {
        if (trimServerToken) return;

        var newData = new byte[data.Length - 4];
        Buffer.BlockCopy(data, 4, newData, 0, newData.Length);
        data = newData;
        length -= 4;
    }

    public override void ProcessOutBoundPacket(ref IPEndPoint endPoint, ref byte[] data, ref int offset, ref int length)
    {
        
    }
}
