using LiteNetLib;
using LiteNetLib.Utils;
using System.Net;

EventBasedNetListener listener = new EventBasedNetListener();
NetManager server = new NetManager(listener);
server.Start(5005);

listener.ConnectionRequestEvent += request =>
{
    request.Accept();
};

listener.PeerConnectedEvent += peer =>
{
    Console.WriteLine("Client connected: " + peer.Id);
};

listener.NetworkReceiveEvent += DataReceived;

void DataReceived(NetPeer peer, NetPacketReader reader, byte channel, DeliveryMethod deliveryMethod)
{
    Console.WriteLine("Received: " + peer.Id);
}

listener.PeerConnectedEvent += peer =>
{
    Console.WriteLine("We got connection: {0}", peer);
    NetDataWriter writer = new NetDataWriter();
    writer.Put("Your id is: " + peer.Id + "\nServer is " + Dns.GetHostName());
    peer.Send(writer, DeliveryMethod.ReliableOrdered);
};
Console.WriteLine("Server started at port 5005");

AppDomain.CurrentDomain.ProcessExit += (s, e) =>
{
    server.Stop();
};

while (true)
{
    server.PollEvents();
    Thread.Sleep(10);
}
