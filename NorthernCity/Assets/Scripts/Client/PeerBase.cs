using LiteNetLib;
using LiteNetLib.Utils;

public class PeerBase
{
    protected EventBasedNetListener listener;
    protected NetManager netManager;

    public virtual void Setup(int port)
    {
        listener = new EventBasedNetListener();
        netManager = new NetManager(listener);
    }

    public static void SendString(NetPeer peer, string message)
    {
        NetDataWriter writer = new NetDataWriter();         
        writer.Put(message);                                
        peer.Send(writer, DeliveryMethod.ReliableOrdered);
    }

    public void Update()
    {
        netManager.PollEvents();
    }

    public void Shutdown()
    {
        netManager.Stop();
    }
}
