using System;

namespace NorthernCityServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Server server = new Server();
            server.Setup(5060, "NorthernCity");
            server.Run();
        }
    }
}
