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
            string[] Answers = { "Host Server Locally - 127.0.0.1:2222", "Host Server - Enter IP:Port" };
            int StartPos = 0;
            bool IpInitialized = false;
            while (!IpInitialized)
            {
                switch (Interface.AnswerInterface("Setting up:", Answers, StartPos))
                {
                    case 0:

                        Server.Host = "127.0.0.1";
                        Server.Port = 2222;
                        IpInitialized = true;
                        break;
                    case 1:
                        string[] Adress;
                        if (!Interface.GetIPAndPort(Answers, 1, out Adress))
                        {
                            StartPos = 1;
                            break;
                        }
                        Server.Host = Adress[0];
                        Server.Port = Int32.Parse(Adress[1]);
                        IpInitialized = true;
                        break;
                }
            }

            Console.Clear();
            Console.SetCursorPosition(0, 0);
            Console.WriteLine($"Initializing Server Components... ");

            IPAddress address = IPAddress.Parse(Server.Host);
            RSA.Generate(out Server.RSAPublicKey, out Server.RSAPrivateKey);
            ECDH.ECDHProvider = new System.Security.Cryptography.ECDiffieHellmanCng(256);
            ECDH.GeneratePublicKey(out Server.ECDHPublicKey);

            Server.ServerSocket = new Socket(address.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            Server.ServerSocket.Bind(new IPEndPoint(address, Server.Port));
            Server.ServerSocket.Listen(100);
            Console.WriteLine($"Server has been started on {Server.Host}:{Server.Host}");
            Console.WriteLine("Waiting for connections...");
            while (Server.Work)
            {
                Socket handle = Server.ServerSocket.Accept();
                Console.WriteLine($"New connection: {handle.RemoteEndPoint.ToString()}");
                new User(handle);

            }
            Console.WriteLine("Server closing...");

        }
    }
}
