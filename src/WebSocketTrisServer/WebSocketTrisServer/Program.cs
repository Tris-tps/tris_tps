using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Diagnostics;
using System.IO.Compression;
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
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Program.ConnectedClientIDs.Remove(ID);
            Console.WriteLine($"Il client {ID} si è disconnesso");
        }

        protected override void OnMessage(MessageEventArgs e)
        {
            Program.MessageHandler(ID, e.Data);
        }
    }

    public class Program
    {
        public static List<string> ConnectedClientIDs = ConnectedClientIDs = new();
        public static bool _winOrDrawBool = false;
        public static WebSocketServer server;
        public static WebSocketServiceHost serviceHost;
        public static char[] board = new char[9] { '#', '#', '#', '#', '#', '#', '#', '#', '#' };
        private static bool isPlayer1Turn = true;
        public static string zucche = "";
        public static int[] posizione = new int[2];
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
        public static List<string> AuthenticatedClients = new List<string>();
        private static bool loginIsFinished = false;
        private static int indexOfCell;
        private static bool isPlayingWithBot = default;
        private static bool playerHasMoved = false;

        public static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, 16);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        public static void ThreadZucche()
        {
            while (true)
            {
                zucche = zucche + "\U0001F383";
                Thread.Sleep(50000);
                if (zucche.Length >= 12)
                {
                    zucche = "";
                    ClearCurrentConsoleLine();
                }
                posizione[0] = Console.CursorLeft;
                posizione[1] = Console.CursorTop;
                Console.SetCursorPosition(0, 16);
                Console.Write(zucche);
                Console.SetCursorPosition(posizione[0], posizione[1]);
            }
        }

        public static void Main(string[] args)
        {

            Thread threadWhileTrue = new Thread(() =>
            {
                while (true) { }
            });

            threadWhileTrue.Start();

            Thread tZucche = new Thread(ThreadZucche); // Passa il riferimento del metodo, senza chiamare il metodo
            tZucche.Start();
            server = new WebSocketServer("ws://127.0.0.1:5000");
            server.AddWebSocketService("/", () => //inizializer del server, inserisce i servizi
            {
                return new Echo();
            });
            Console.WriteLine("server");
            server.Start();

            serviceHost = server.WebSocketServices["/"];

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            while (ConnectedClientIDs.Count < 1)
            {
                Console.WriteLine("In attesa che il primo client si connetta...");
                Thread.Sleep(1000);
            }

            currentPlayerID = ConnectedClientIDs[0];

            // Richiedi login o registrazione
            SendMessage("Effettua il login o registrati.", currentPlayerID);
            SendMessage("login", currentPlayerID);

            while (AuthenticatedClients.Count < 1)
            {
                Console.WriteLine("In attesa che il primo client faccia il login...");
                Thread.Sleep(1000);
            }

            SendMessage("?vuoi giocare con il bot o con un'altra persona? \n a) bot \n b) persona", currentPlayerID);
            //Thread.Sleep(2000); dovrei aspettare che specifichi se vuole giocare con il client o con il server

            if (!isPlayingWithBot)
            {
                while (ConnectedClientIDs.Count < 2)
                {
                    Console.WriteLine("In attesa che i client si connettano...");
                    Thread.Sleep(200);
                }
                SendMessage("Effettua il login o registrati.", ConnectedClientIDs[1]);
                SendMessage("login", ConnectedClientIDs[1]);
                while (AuthenticatedClients.Count < 2)
                {
                    Console.WriteLine("In attesa che il secondo client faccia il login...");
                    Thread.Sleep(1000);
                }
                if (AuthenticatedClients.Count == 2)
                    loginIsFinished = true;
                InitializeGame();
            }
        }

        public static void InitializeGame()
        {
            //invio il messaggio di inizio partita ai client
            SendMessage("La partita è iniziata", ConnectedClientIDs[0]);
            if (!isPlayingWithBot)
                SendMessage("La partita è iniziata", ConnectedClientIDs[1]);
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
            SendMessage(BoardConvert(), currentPlayerID);
        }

        public static void RequestMove(string ID)
        {
            SendMessage("é il tuo turno, digita la tua mossa!", ID);
            SendMessage("+", ID); //mando al client il "segnale", il quale specifica che è il suo turno, vedere nel client l'if del "+"
        }

        public static void SendWinMessages(string winPlayerId, string looserPlayerId)
        {
            _winOrDrawBool = true;
            SendMessage("Hai Vinto!", winPlayerId);
            SendMessage("Hai Perso!", looserPlayerId);
        }

        public static void SendMessage(string message, string ID)
        {
            serviceHost.Sessions.SendTo(message, ID);
        }

        public static bool CheckWin()
        {
            foreach (var combination in _winningCombinations)
            {
                if (combination.All(index => board[index] == 'X'))
                {
                    SendWinMessages(ConnectedClientIDs[0], ConnectedClientIDs[1]);
                    return true;
                }
                if (combination.All(index => board[index] == 'O'))
                {
                    SendWinMessages(ConnectedClientIDs[1], ConnectedClientIDs[0]);
                    return true;
                }
            }
            return false;
        }

        private static bool CheckDraw()
        {
            foreach (var cell in board)
            {
                if (cell == '#')
                    return false;
            }
            _winOrDrawBool = true;
            SendMessage("La partita è finita in pareggio", ConnectedClientIDs[0]);
            SendMessage("La partita è finita in pareggio", ConnectedClientIDs[1]);
            return true;
        }

        public static void Game(int indexOfCell, string ID)
        {
            if (board[indexOfCell] == '#')
            {
                if (ConnectedClientIDs[0].Equals(ID))
                    board[indexOfCell] = 'X';
                else if (ConnectedClientIDs[1].Equals(ID))
                    board[indexOfCell] = 'O';

                // Passa il turno all'altro giocatore
                Console.WriteLine("Il player ha fatto la mossa");
                playerHasMoved = true;
                isPlayer1Turn = !isPlayer1Turn;
                if(!isPlayingWithBot)
                {
                    currentPlayerID = isPlayer1Turn ? ConnectedClientIDs[0] : ConnectedClientIDs[1];
                    Print();
                }   
                    
                CheckWin();
                CheckDraw();

                if (_winOrDrawBool)
                {
                    Print();
                    return;
                }

                if (isPlayingWithBot)
                {
                    if (playerHasMoved)
                    {
                        int botIndex = Bot.BotMove(board);
                        board[botIndex] = 'O';
                        playerHasMoved = !playerHasMoved;
                        Print();
                        RequestMove(ConnectedClientIDs[0]);
                    }
                    return;
                }
                RequestMove(currentPlayerID);
            }
            else
            {
                Console.WriteLine("La cella è già occupata.");
                SendMessage("La cella è già occupata", ID);
                RequestMove(currentPlayerID);
            }
        }

        public static void MessageHandler(string ID, object message)
        {
            if ((string)message == "a")
            {
                //bot 
                isPlayingWithBot = true;
                ConnectedClientIDs.Add("Bot");
                RequestMove(ID);
                //PlayWithBot();
            }

            if (ID != currentPlayerID && loginIsFinished)
            {
                Console.WriteLine($"Non è il tuo turno, giocatore {ID}");
                SendMessage("Non è il tuo turno", ID);
                return;
            }

            if ((int.TryParse((string)message, out indexOfCell) && indexOfCell >= 1 && indexOfCell <= 9))
            {
                indexOfCell--; // Adatto l'indice della cella alla rappresentazione (0-8)
                Game(indexOfCell, ID);
            }

            if (message.ToString().StartsWith("login:") || message.ToString().StartsWith("register:"))
            {
                RequestLogin(ID, (string)message);
                return;
            }
        }

        private static void RequestLogin(string ID, string userInput)
        {
            string[] inputParts = userInput.Split(':');

            if (inputParts.Length == 2)
            {
                string action = inputParts[0].ToLower();
                string username = inputParts[1];
                if (action == "login")
                {
                    if (AuthenticatedClients.Count != 0 && AuthenticatedClients[0] == username)
                    {
                        SendMessage($"L'utente {username} ha già fatto il login da un altro dispositivo", ID);
                        SendMessage("login", ID);
                    }
                    else if (!_login.AuthenticateUser(username))
                    {
                        SendMessage($"Utente {username} non registrato", ID);
                        SendMessage("login", ID);
                    }
                    else if (_login.AuthenticateUser(username))
                    {
                        SendMessage($"Hai fatto il login!", ID);
                        AuthenticatedClients.Add(username);
                    }
                }
                else if (action == "register")
                {
                    if (_login.RegisterUser(username))
                    {
                        AuthenticatedClients.Add(username);
                        SendMessage($"Utente {username} registrato", ID);
                    }
                    else if (!_login.RegisterUser(username))
                    {
                        SendMessage($"Utente {username} già esistente, fai il login", ID);
                        SendMessage("login", ID);
                    }
                }
                else
                {
                    SendMessage("Comando non valido.", ID);
                    SendMessage("login", ID);

                }
            }
            else
            {
                SendMessage("Input non valido. Assicurati di inserire 'login:username' o 'register:username'.", ID);
                SendMessage("login", ID);
            }
        }
    }
}