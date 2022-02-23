using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteNetLib;
using LiteNetLib.Utils;

namespace NorthernCityServer
{
    class PeerBase
    {
        protected EventBasedNetListener listener;
        protected NetManager netManager;

        public virtual void Setup(int port, string key)
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
}
