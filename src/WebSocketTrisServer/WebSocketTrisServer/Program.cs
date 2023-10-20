using System;
using System.Net;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace WebSocketTrisServer
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            TcpListener server = new TcpListener(IPAddress.Parse("127.0.0.1"), 5000);
            server.Start();
            Console.WriteLine("Server has started on 127.0.0.1:80.\nWaiting for a connection...");

            TcpClient client = await server.AcceptTcpClientAsync();
            Console.WriteLine("A client connected.");

            NetworkStream stream = client.GetStream();

            //enter to an infinite cycle to be able to handle every change in stream
            while (true)
            {
                while (!stream.DataAvailable) ;

                byte[] bytes = new byte[client.Available];

                stream.Read(bytes, 0, bytes.Length);
            }
        }
    }
}