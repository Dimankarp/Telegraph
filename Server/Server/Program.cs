using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse(Server.Host);
            RSA.Generate(out Server.RSAPublicKey, out Server.RSAPrivateKey);
            ECDH.ECDHProvider = new System.Security.Cryptography.ECDiffieHellmanCng(256);
            ECDH.GeneratePublicKey(out Server.ECDHPublicKey);
            Server.ServerSocket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Server.ServerSocket.Bind(new IPEndPoint(address, Server.Port));
            Server.ServerSocket.Listen(100);
            Console.WriteLine($"Server has been started on {Server.Host}:{Server.Port}");
            Console.WriteLine("Waiting for connections...");
            while(Server.Work)
            {
                Socket handle = Server.ServerSocket.Accept();
                Console.WriteLine($"New connection: {handle.RemoteEndPoint.ToString()}");
                new User(handle);

            }
            Console.WriteLine("Server closing...");

        }
    }
}
