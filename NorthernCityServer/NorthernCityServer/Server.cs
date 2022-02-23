using System;
using System.Collections.Generic;
using System.Threading;
using LiteNetLib;
using LiteNetLib.Utils;
using ProtoMessages;

namespace NorthernCityServer
{
    class Server : PeerBase
    {
        private static NetManager server;

        public Action peerConnected;
        Dictionary<MessageType, Action<string>> actions = new Dictionary<MessageType, Action<string>>();

        public override void Setup(int port, string key) 
        {
            base.Setup(port, key);

            netManager.Start(port);

            server = netManager;

            listener.ConnectionRequestEvent += request =>
            {
                if (netManager.ConnectedPeersCount < 10)
                    request.AcceptIfKey(key);
                else
                    request.Reject();
            };

            listener.NetworkReceiveEvent += OnRequest;

            listener.PeerConnectedEvent += peer =>
            {
                Console.WriteLine("We got connection: {0}", peer.EndPoint); // Show peer ip
            };
        }

        public void Run()
        {
            while (!Console.KeyAvailable)
            {
                Update();
                Thread.Sleep(15);
            }
        }


        public void AddAction(Action<string> action, MessageType type)
        {
            if (actions.ContainsKey(type))
                actions[type] += action;
            else
                actions.Add(type, action);
        }

        public void RemoveAction(Action<string> action, MessageType type)
        {
            if (actions.ContainsKey(type))
            {
                actions[type] -= action;
                if (actions[type] == null) actions.Remove(type);
            }
        }

        public void OnRequest(NetPeer fromPeer, NetPacketReader dataReader, DeliveryMethod deliveryMethod)
        {
            NetworkMessage message = NetworkMessage.Parser.ParseJson(dataReader.GetString(1000));
            if (actions.ContainsKey(message.Type)) actions[message.Type]?.Invoke(message.Message);
        }

        static public void SendMessage(string message)
        {
            NetDataWriter writer = new NetDataWriter();
            writer.Put(message);
            server.SendToAll(writer, DeliveryMethod.ReliableOrdered);
        }


    }
}
