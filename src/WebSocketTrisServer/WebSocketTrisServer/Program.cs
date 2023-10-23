using System.Net;
using System.Net.Sockets;

namespace WebSocketTrisServer
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Controller controller = new Controller(new Model());
            var ipe = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            var listener = new Socket(ipe.AddressFamily,
                SocketType.Stream,
                ProtocolType.Tcp);

            listener.Bind(ipe);
            listener.Listen();

            List<Task> tasks = new List<Task>();

            for (int i = 0; i < 3; i++)
            {
                var socket = listener.Accept();
                Console.WriteLine("Client connected");
                var view = new VirtualView(socket);
                controller.AddView(view);

                tasks.Add(Task.Run(view.Run));
            }

            controller.Start();

            Task.WaitAll(tasks.ToArray());
        }
    }
}