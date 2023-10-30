using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Data;
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
        private static bool _winOrDrawBool = false;
        private static WebSocketServer server;
        private static WebSocketServiceHost serviceHost;
        private static char[] board = new char[9] { '#', '#', '#', '#', '#', '#', '#', '#', '#' };
        private static bool _isPlayer1Turn = true;
        private static string zucche = "";
        private static int[] posizione = new int[2];
        private static string _currentPlayerId = "";
        private static readonly int[][] WinningCombinations = new int[][]
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
        private static readonly Login Login = new Login();
        private static List<string> _authenticatedClients = new List<string>();
        private static bool _loginIsFinished = false;
        private static int _indexOfCell;
        private static bool _isPlayingWithBot = default;
        private static bool _playerHasMoved = false;
        private static ManualResetEventSlim _waitForClientResponse = new ManualResetEventSlim(false);

        private static void ClearCurrentConsoleLine()
        {
            int currentLineCursor = Console.CursorTop;
            Console.SetCursorPosition(0, 16);
            Console.Write(new string(' ', Console.WindowWidth));
            Console.SetCursorPosition(0, currentLineCursor);
        }

        private static void ThreadZucche()
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

            InitializeWebSocketServer();

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            HandleFirstClientAccess();

            AskForGameMode();
            
            if (!_isPlayingWithBot)
            {
                HandleSecondClientAccess();
            }
        }

        private static void InitializeWebSocketServer()
        {
            server = new WebSocketServer("ws://127.0.0.1:5000");
            server.AddWebSocketService("/", () => //inizializer del server, inserisce i servizi
            {
                return new Echo();
            });
            Console.WriteLine("server");
            server.Start();

            serviceHost = server.WebSocketServices["/"];
        }

        private static void HandleFirstClientAccess()
        {
            while (ConnectedClientIDs.Count < 1)
            {
                Console.WriteLine("In attesa che il primo client si connetta...");
                Thread.Sleep(1000);
            }

            _currentPlayerId = ConnectedClientIDs[0];

            // Richiedi login o registrazione
            //SendMessage("Effettua il login o registrati.", _currentPlayerId);
            SendMessage("login", _currentPlayerId);

            while (_authenticatedClients.Count < 1)
            {
                Console.WriteLine("In attesa che il primo client faccia il login...");
                Thread.Sleep(1000);
            }
        }

        private static void AskForGameMode()
        {
            // Richiedi se giocare con un bot o un'altra persona
            SendMessage("?vuoi giocare con il bot o con un'altra persona? \n a) bot \n b) persona", _currentPlayerId);
            _waitForClientResponse.Wait(); //aspetto che il client1 specifichi se vuole giocare con il client o con il bot
        }

        private static void HandleSecondClientAccess()
        {
            while (ConnectedClientIDs.Count < 2)
            {
                Console.WriteLine("In attesa che i client si connettano...");
                Thread.Sleep(200);
            }
            SendMessage("Effettua il login o registrati.", ConnectedClientIDs[1]);
            SendMessage("login", ConnectedClientIDs[1]);
            while (_authenticatedClients.Count < 2)
            {
                Console.WriteLine("In attesa che il secondo client faccia il login...");
                Thread.Sleep(1000);
            }
            if (_authenticatedClients.Count == 2)
                _loginIsFinished = true;
            InitializeGame();
        }

        private static void InitializeGame()
        {
            //invio il messaggio di inizio partita ai client
            SendMessage("La partita è iniziata", ConnectedClientIDs[0]);
            if (!_isPlayingWithBot)
                SendMessage("La partita è iniziata", ConnectedClientIDs[1]);
            //caso ipotetico dove inizia il client ConnectedClientIDs[0]
            Thread.Sleep(100);
            Print();
            Thread.Sleep(100);
            RequestMove(_currentPlayerId);
        }

        private static string BoardConvert()
        {
            string table = "*";
            for (int i = 0; i < board.Count(); i++)
            {
                table += board[i];
            }
            return table;
        }

        private static void Print()
        {
            SendMessage(BoardConvert(), _currentPlayerId);
        }

        private static void RequestMove(string ID)
        {
            if (_winOrDrawBool)
                return;
            SendMessage("é il tuo turno, digita la tua mossa!", ID);
            SendMessage("+", ID); //mando al client il "segnale", il quale specifica che è il suo turno, vedere nel client l'if del "+"
        }

        private static void SendWinMessages(string winPlayerId, string looserPlayerId)
        {
            _winOrDrawBool = true;
            if(!_isPlayingWithBot)
                SendMessage("Hai Vinto!", winPlayerId);
            SendMessage("Hai Perso!", looserPlayerId);
        }

        private static void SendMessage(string message, string ID)
        {
            serviceHost.Sessions.SendTo(message, ID);
        }

        private static void CheckWin()
        {
            foreach (var combination in WinningCombinations)
            {
                if (combination.All(index => board[index] == 'X'))
                    SendWinMessages(ConnectedClientIDs[0], ConnectedClientIDs[1]);
                else if (combination.All(index => board[index] == 'O'))
                    SendWinMessages(ConnectedClientIDs[1], ConnectedClientIDs[0]);
            }
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
            if(!_isPlayingWithBot)
                SendMessage("La partita è finita in pareggio", ConnectedClientIDs[1]);
            return true;
        }

        private static void Game(int indexOfCell, string ID)
        {
            if (_winOrDrawBool)
            {
                Print();
                return;
            }

            if (board[indexOfCell] == '#')
            {
                MakeMove(indexOfCell, ID);
            }
            else
            {
                HandleOccupiedCell(ID);
            }
        }

        private static void MakeMove(int indexOfCell, string ID)
        {
            char playerSymbol = ConnectedClientIDs[0].Equals(ID) ? 'X' : 'O';
            board[indexOfCell] = playerSymbol;

            // Passa il turno all'altro giocatore
            Console.WriteLine("Il player ha fatto la mossa");
            _playerHasMoved = true;
            _isPlayer1Turn = !_isPlayer1Turn;

            if (!_isPlayingWithBot)
            {
                Print();
                _currentPlayerId = _isPlayer1Turn ? ConnectedClientIDs[0] : ConnectedClientIDs[1];
                Print();
            }

            CheckWin();
            CheckDraw();

            if (_isPlayingWithBot && _playerHasMoved)
            {
                int botIndex = Bot.BotMove(board);
                board[botIndex] = 'O';
                CheckWin();
                _playerHasMoved = !_playerHasMoved;
                Print();

                if (_winOrDrawBool)
                {
                    return;
                }

                RequestMove(ConnectedClientIDs[0]);
            }
            else if (!_isPlayingWithBot)
            {
                RequestMove(_currentPlayerId);
            }
        }

        private static void HandleOccupiedCell(string ID)
        {
            Console.WriteLine("La cella è già occupata.");
            SendMessage("La cella è già occupata", ID);
            RequestMove(_currentPlayerId);
        }

        public static void MessageHandler(string ID, object message)
        {
            string messageString = message.ToString();

            if (messageString == "a")
            {
                HandleBotSelection(ID);
            }
            else if (messageString == "b")
            {
                HandlePlayerSelection(ID);
            }
            else if (ID != _currentPlayerId && _loginIsFinished)
            {
                HandleInvalidTurn(ID);
                return;
            }
            else if (int.TryParse(messageString, out _indexOfCell) && _indexOfCell >= 1 && _indexOfCell <= 9)
            {
                HandleGameMove(ID);
            }
            else if (messageString.StartsWith("login:") || messageString.StartsWith("register:"))
            {
                RequestLogin(ID, messageString);
            }
        }

        private static void HandleBotSelection(string ID)
        {
            _waitForClientResponse.Set();
            _isPlayingWithBot = true;
            ConnectedClientIDs.Add("Bot");
            RequestMove(ID);
        }

        private static void HandlePlayerSelection(string ID)
        {
            _waitForClientResponse.Set();
        }

        private static void HandleInvalidTurn(string ID)
        {
            Console.WriteLine($"Non è il tuo turno, giocatore {ID}");
            SendMessage("Non è il tuo turno", ID);
        }

        private static void HandleGameMove(string ID)
        {
            _indexOfCell--; // Adatto l'indice della cella alla rappresentazione (0-8)
            Game(_indexOfCell, ID);
        }

        /*
        public static void MessageHandler(string ID, object message)
        {
            if ((string)message == "a")
            {
                //bot 
                _waitForClientResponse.Set();
                _isPlayingWithBot = true;
                ConnectedClientIDs.Add("Bot");
                RequestMove(ID);
            }
            else if((string)message == "b")
                _waitForClientResponse.Set();

            if (ID != _currentPlayerId && _loginIsFinished)
            {
                Console.WriteLine($"Non è il tuo turno, giocatore {ID}");
                SendMessage("Non è il tuo turno", ID);
                return;
            }

            if ((int.TryParse((string)message, out _indexOfCell) && _indexOfCell >= 1 && _indexOfCell <= 9))
            {
                _indexOfCell--; // Adatto l'indice della cella alla rappresentazione (0-8)
                Game(_indexOfCell, ID);
            }

            if (message.ToString().StartsWith("login:") || message.ToString().StartsWith("register:"))
            {
                RequestLogin(ID, (string)message);
                return;
            }
        }
        */

        private static void RequestLogin(string ID, string userInput)
        {
            string[] inputParts = userInput.Split(':');

            if (inputParts.Length != 2)
            {
                SendMessage("Input non valido. Assicurati di inserire 'login:username' o 'register:username'.", ID);
                SendMessage("login", ID);
                return;
            }

            string action = inputParts[0].ToLower();
            string username = inputParts[1];

            switch (action)
            {
                case "login":
                    HandleLogin(username, ID);
                    break;
                case "register":
                    HandleRegistration(username, ID);
                    break;
                default:
                    SendMessage("Comando non valido.", ID);
                    SendMessage("login", ID);
                    break;
            }
        }

        private static void HandleLogin(string username, string ID)
        {
            if (_authenticatedClients.Count != 0 && _authenticatedClients[0] == username)
            {
                SendMessage($"L'utente {username} ha già fatto il login da un altro dispositivo", ID);
                SendMessage("login", ID);
            }
            else if (!Login.AuthenticateUser(username))
            {
                SendMessage($"Utente {username} non registrato", ID);
                SendMessage("login", ID);
            }
            else
            {
                SendMessage($"Hai fatto il login!", ID);
                _authenticatedClients.Add(username);
            }
        }

        private static void HandleRegistration(string username, string ID)
        {
            if (Login.RegisterUser(username))
            {
                _authenticatedClients.Add(username);
                SendMessage($"Utente {username} registrato", ID);
            }
            else
            {
                SendMessage($"Utente {username} già esistente, fai il login", ID);
                SendMessage("login", ID);
            }
        }
    }
}