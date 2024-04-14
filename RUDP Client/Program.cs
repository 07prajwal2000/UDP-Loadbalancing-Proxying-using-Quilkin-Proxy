using LiteNetLib;
using RUDPClient;

EventBasedNetListener listener = new EventBasedNetListener();
NetManager client = new NetManager(listener, new BasePacketLayer());
//NetManager client = new NetManager(listener);
client.Start();
client.Connect("localhost", 5008, "");

listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod, channel) =>
{
    Console.WriteLine(dataReader.GetString(100));
    dataReader.Recycle();
};

while (!Console.KeyAvailable)
{
    client.PollEvents();
    Thread.Sleep(15);
}

client.Stop();