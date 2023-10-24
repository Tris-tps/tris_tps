using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using WebSocketTrisServer;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            var ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            var socket = new Socket(ipe.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            socket.Connect(ipe);

            var reader = new StreamReader(new NetworkStream(socket));
            var writer = new StreamWriter(new NetworkStream(socket));
            writer.AutoFlush = true;


            while (true)
            {
                var msg = JsonSerializer.Deserialize<Message>(reader.ReadLine());

                switch (msg.MessageCode)
                {
                    case Message.Code.StartGame:
                        {
                            Console.WriteLine($"La partita è iniziata");
                            break;
                        }
                }
            }
        }
    }
}