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
        public static List<string> ConnectedClientIDs = ConnectedClientIDs = new();
        private static char[] board = new char[9] {'#', '#', '#', '#', '#', '#', '#', '#', '#'};
        private static bool isPlayer1Turn = true;
        private static string currentPlayerID = "";
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
            while (ConnectedClientIDs.Count < 1) 
            {
                Console.WriteLine("In attesa del client");
                Thread.Sleep(100);
            }
            currentPlayerID = ConnectedClientIDs[0];
            //aspetto che almeno due client (1 per prova) si connettanno
            serviceHost.Sessions.SendTo(currentPlayerID, currentPlayerID); //invio il messaggio che contiene l'id del client al client
            serviceHost.Sessions.SendTo("La partita è iniziata", currentPlayerID); //invio il messaggio di inizio partita ai client
            //caso ipotetico dove inizia il client ConnectedClientIDs[0]
            serviceHost.Sessions.SendTo("Tocca a te", currentPlayerID);
            serviceHost.Sessions.SendTo(BoardConvert(), currentPlayerID);
        }
        
        public static string BoardConvert()
        {
            string tabella = "*";
            for (int i = 0; i < board.Count(); i++)
            {
                tabella += board[i];
            }
            return tabella;
        }

        public static void MessageHandler(string ID, object message)
        {
            if (ID != currentPlayerID)
            {
                Console.WriteLine($"Non è il tuo turno, giocatore {ID}");
                return;
            }

            int indexOfCell;
            if (int.TryParse((string)message, out indexOfCell) && indexOfCell >= 1 && indexOfCell <= 9)
            {
                indexOfCell--; // Adatto l'indice della cella alla rappresentazione (0-8)

                if (board[indexOfCell] == '#')
                {
                    if (ConnectedClientIDs[0].Equals(ID))
                        board[indexOfCell] = 'X';
                    else if (ConnectedClientIDs[1].Equals(ID))
                        board[indexOfCell] = 'O';

                    // Passa il turno all'altro giocatore
                    isPlayer1Turn = !isPlayer1Turn;
                    currentPlayerID = isPlayer1Turn ? ConnectedClientIDs[0] : ConnectedClientIDs[1];

                    for (int i = 0; i < board.Length; i++)
                    {
                        Console.Write("a" + board[i] + "\t");
                    }
                }
                else
                {
                    Console.WriteLine("La cella è già occupata.");
                }
            }
            else
            {
                Console.WriteLine("Mossa non valida.");
            }

            /*
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
            else
            {
                //TODO: il server invia messaggio di errore la cella è già piena
            }
            for (int i = 0; i < board.Length; i++)
            {
                Console.Write("a" + board[i] + "\t");
            }
            */
        }
    }
}