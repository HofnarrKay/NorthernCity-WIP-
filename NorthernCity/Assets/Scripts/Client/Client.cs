using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Client : PeerBase
{
    protected static Client instance;

    public static Client Instance
    {
        get => instance;
    }


    public static void Setup(string ip, int port, string key)
    {
        instance = new Client();

        instance.Setup(port);

        instance.netManager.Start();
        instance.netManager.Connect(ip, port, key);
        instance.listener.NetworkReceiveEvent += (fromPeer, dataReader, deliveryMethod) =>
        {
            var message = dataReader.GetString();
            dataReader.Recycle();
        };
    }

    public void SendStringToServer(string message)
    {
        SendString(netManager.FirstPeer, message);
    }
}
