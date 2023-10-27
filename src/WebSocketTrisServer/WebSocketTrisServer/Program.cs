﻿using System;
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
        public static bool _winBool = false;
        public static WebSocketServer server;
        public static WebSocketServiceHost serviceHost;
        private static char[] board = new char[9] {'#', '#', '#', '#', '#', '#', '#', '#', '#'};
        private static bool isPlayer1Turn = true;
        private static string currentPlayerID = "";
        private static readonly int[][] _winningCombinations = new int[][]
        {
            new int[] {0, 1, 2}, // Righe
            new int[] {3, 4, 5},
            new int[] {6, 7, 8},
            new int[] {0, 3, 6}, // Colonne
            new int[] {1, 4, 7},
            new int[] {2, 5, 8},
            new int[] {0, 4, 8}, // Diagonali
            new int[] {2, 4, 6}
        };
        private static Login _login = new Login();

        private static void Main(string[] args)
        {
            Thread threadWhileTrue = new Thread(() =>
            {
                while (true) { }
            });
            threadWhileTrue.Start();
            server = new WebSocketServer("ws://127.0.0.1:5000");
            server.AddWebSocketService("/", () => //inizializer del server, inserisce i servizi
            {
                return new Echo();
            });
            Console.WriteLine("server");
            server.Start();
            
            serviceHost = server.WebSocketServices["/"];
            while (ConnectedClientIDs.Count < 2) //aspetto che i 2 client si connettanno
            {
                Console.WriteLine("In attesa del client");
                Thread.Sleep(200);
            }
            currentPlayerID = ConnectedClientIDs[0];
            RequestLogin(ConnectedClientIDs[0]);
            RequestLogin(ConnectedClientIDs[1]);
            
            //invio il messaggio di inizio partita ai client
            serviceHost.Sessions.SendTo("La partita è iniziata", ConnectedClientIDs[0]); 
            serviceHost.Sessions.SendTo("La partita è iniziata", ConnectedClientIDs[1]);
            //caso ipotetico dove inizia il client ConnectedClientIDs[0]
            Thread.Sleep(100);
            Print();
            Thread.Sleep(100);
            RequestMove(currentPlayerID);
        }

        public static string BoardConvert()
        {
            string table = "*";
            for (int i = 0; i < board.Count(); i++)
            {
                table += board[i];
            }
            return table;
        }

        public static void Print()
        {
            serviceHost.Sessions.SendTo(BoardConvert(), currentPlayerID);
        }

        public static void RequestMove(string ID)
        {
            serviceHost.Sessions.SendTo("é il tuo turno, digita la tua mossa!", ID);
            serviceHost.Sessions.SendTo("+", ID); //mando al client il "segnale", il quale specifica che è il suo turno, vedere nel client l'if del "+"
        }

        public static void SendWinMessages(string winPlayerId, string looserPlayerId)
        {
            _winBool = true;
            serviceHost.Sessions.SendTo("Hai vinto!!!", winPlayerId);
            serviceHost.Sessions.SendTo("Hai perso", looserPlayerId);
            Console.WriteLine("hai vinto");
        }

        public static bool CheckWin()
        {
            foreach (var combination in _winningCombinations)
            {
                if (combination.All(index => board[index] == 'X'))
                {
                    SendWinMessages(ConnectedClientIDs[0], ConnectedClientIDs[1]); 
                    return true; // Abbiamo una combinazione vincente
                }
                if(combination.All(index => board[index] == 'O'))
                {
                    SendWinMessages(ConnectedClientIDs[1], ConnectedClientIDs[0]);
                    return true; // Abbiamo una combinazione vincente
                }
            }
            return false;
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
                    Console.WriteLine("Il player ha fatto la mossa");
                    isPlayer1Turn = !isPlayer1Turn;
                    currentPlayerID = isPlayer1Turn ? ConnectedClientIDs[0] : ConnectedClientIDs[1];
                    CheckWin();
                    Print();
                    if (_winBool)
                    {
                        return;
                    }
                    RequestMove(currentPlayerID);
                }
                else
                {
                    Console.WriteLine("La cella è già occupata.");
                    serviceHost.Sessions.SendTo("La cella è già occupata", ID);
                    RequestMove(currentPlayerID);

                }
            }
            else
            {
                Console.WriteLine("Mossa non valida.");
                serviceHost.Sessions.SendTo("Mossa non valida.", ID);
                RequestMove(currentPlayerID);
            }
        }

        private static void RequestLogin(string ID)
        {
            // Richiedi login o registrazione
            serviceHost.Sessions.SendTo("Benvenuto! Effettua il login o registrati.", ID);
            serviceHost.Sessions.SendTo("Inserisci 'login:username:password' per effettuare il login.", ID);
            serviceHost.Sessions.SendTo("Inserisci 'register:username:password' per registrarti.", ID);
        }
    }
}