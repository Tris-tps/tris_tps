using System;
using System.Net;
using System.Net.Sockets;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketTrisServer
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnMessage(MessageEventArgs e)
        {
            Console.WriteLine(e.Data);

        }
    }

    public class Program
    {
        private static void Main(string[] args)
        {
            //    Controller controller = new Controller(new Model());
            //    var ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            //    var listener = new Socket(ipe.AddressFamily,
            //        SocketType.Stream,
            //        ProtocolType.Tcp);
            //    listener.Bind(ipe);
            //    listener.Listen();

            //    List<Task> tasks = new List<Task>();

            //    for (int i = 0; i < 1; i++) //i<3
            //    {
            //        var socket = listener.Accept();
            //        Console.WriteLine("Client connected");
            //        var view = new VirtualView(socket);
            //        controller.AddView(view);
            //        tasks.Add(Task.Run(view.Run));
            //    }

            //    controller.Start();

            //    Task.WaitAll(tasks.ToArray());

            WebSocketServer server = new WebSocketServer("ws://127.0.0.1:5000");
            server.AddWebSocketService("/", () => //inizializer del server, inserisce i servizi
            {
                return new Echo();
            });
            Console.WriteLine("server");
            server.Start();
            while (true) { };
        }
    }
}