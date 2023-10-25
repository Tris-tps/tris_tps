using System;
using System.Net;
using System.Net.Sockets;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace WebSocketTrisServer
{
    public class Echo : WebSocketBehavior
    {
        protected override void OnOpen()
        {
            Program.ConnectedClientIDs.Add(ID);
            Console.WriteLine($"Il client {ID} si è connesso");
            //Send("ciao sono il server ti sei appena connesso");
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Program.ConnectedClientIDs.Remove(ID);
            Console.WriteLine($"Il client {ID} si è disconnesso");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            //Console.WriteLine(e.Data);
            Program.MessageHandler(ID, e.Data);
        }
    }

    public class Program
    {
        public static List<string>? ConnectedClientIDs = ConnectedClientIDs = new();
        private static char[] board = new char[9] { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' '};
        private static void Main(string[] args)
        {
            Thread threadWhileTrue = new Thread(() =>
            {
                while (true) { }
            });
            threadWhileTrue.Start();
            WebSocketServer server = new WebSocketServer("ws://127.0.0.1:5000");
            server.AddWebSocketService("/", () => //inizializer del server, inserisce i servizi
            {
                return new Echo();
            });
            Console.WriteLine("server");
            server.Start();
            var serviceHost = server.WebSocketServices["/"];
            //serviceHost.Sessions.SendTo("ciao", ConnectedClientIDs[0]);
        }
        
        public static void MessageHandler(string ID, object message)
        {
            int indexOfCell = int.Parse((string)message);
            indexOfCell -= 1;
            Console.WriteLine($"Il client {ID} ha inviato {(string)message}");
            if (board[indexOfCell] == ' ')
            {
                if (ConnectedClientIDs[0].Equals(ID))
                    board[indexOfCell] = 'X';
                else if (ConnectedClientIDs[1].Equals(ID))
                    board[indexOfCell] = 'O';
            }
            for (int i = 0; i < board.Length; i++)
            {
                Console.Write("a" + board[i] + "\t");
            }
        }
    }
}