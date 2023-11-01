using System;
using System.Collections.Generic;
using System.Threading;
using WebSocketSharp;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;
using NAudio.Wave;

namespace Client_2;

public class Program
{
    private static List<string> board = new List<string>() { "#", "#", "#", "#", "#", "#", "#", "#", "#" };
    public static WebSocket client;
    public static Dictionary<int, int[]> table = new Dictionary<int, int[]>();
    private static string _previousBoard = string.Empty;
    private static bool isGameFinished = false;
    private static bool _isPlayingWithBot = default;

    static void Main(string[] args)
    {
        Thread threadWhileTrue = new Thread(() =>
        {
            while (true) { }
        });
        threadWhileTrue.Start();
        InitializeTable();
        SetupConsole();
        ConnectToServer();
        Thread music = new Thread(() =>
        {
            StartMusic();
        });
        //music.Start();
    }

    private static void StartMusic()
    {
        //setup path for music
        var folder = AppContext.BaseDirectory;
        var musicFilePath = Path.Combine(folder, "..\\..\\..\\music.mp3");

        try
        {
            using (var audioFile = new AudioFileReader(musicFilePath))
            using (var outputDevice = new WaveOutEvent())
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    // Attendi il termine della riproduzione o esegui altre operazioni
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Si è verificato un errore: " + ex.Message);
        }
    }

    private static void SetupConsole()
    {
        Console.Title = "ClientView_1";
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        LoginPage.WriteLogo();
    }

    private static void ConnectToServer()
    {
        client = new WebSocket("ws://127.0.0.1:5000");
        Thread.Sleep(100);
        client.Connect();
        client.OnMessage += Message;
    }

    private static void InitializeTable()
    {
        table.Add(1, new int[] { 24, 2 });   // casella in alto a sinistra
        table.Add(4, new int[] { 24, 10 });  // casella in mezzo a sinistra
        table.Add(7, new int[] { 24, 18 });  // casella in basso a sinistra
        table.Add(2, new int[] { 40, 2 });   // casella in alto al centro
        table.Add(5, new int[] { 40, 10 });  // casella in mezzo al centro
        table.Add(8, new int[] { 40, 18 });  // casella in basso al centro
        table.Add(3, new int[] { 56, 2 });   // casella in alto a destra
        table.Add(6, new int[] { 56, 10 });  // casella in mezzo a destra
        table.Add(9, new int[] { 56, 18 });  // casella in basso a destra
    }

    private static void Message(object? obj, MessageEventArgs e)
    {
        var data = e.Data;

        if (!data.StartsWith('*') && !data.StartsWith('+') && !data.StartsWith('?') && data != "login"
            && data != "Hai Vinto!" && data != "Hai Perso!" && data != "La partita è finita in pareggio"
            && data != "bot") //il ' * ' è utilizzato per identificare che il dato sia la board
        {
            Console.SetCursorPosition(28, 27);
            Console.WriteLine("                                            ");
            Console.SetCursorPosition(28, 27);
            if (data == "È il tuo turno, digita la tua mossa!: ")
            {
                Console.Write(data);
            }
            else
            {
                Console.WriteLine(data);
            }
        }
        else if (data.StartsWith("*") && !isGameFinished)
        {
            PrintBoard(data);
        }
        else if (data == "+")
        {
            MakeMove();
        }
        else if (data.StartsWith("?"))
        {
            ChooseMode(data);
        }
        else if (data == "login")
        {
            LoginManager();
        }
        else if (data == "bot")
        {
            _isPlayingWithBot = true;
        }
        else if (data == "Hai Vinto!")
        {
            Console.Clear();
            ResultsPage.DisplayWin();
            isGameFinished = !isGameFinished;
            if (_isPlayingWithBot)
            {
                DisplayChoose();
                return;
            }
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
        else if (data == "Hai Perso!")
        {
            Console.Clear();
            ResultsPage.DisplayLose();
            isGameFinished = !isGameFinished;
            if (_isPlayingWithBot)
            {
                DisplayChoose();
                return;
            }
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
        else if (data == "La partita è finita in pareggio")
        {
            Console.Clear();
            ResultsPage.DisplayDraw();
            isGameFinished = !isGameFinished;
            if (_isPlayingWithBot)
            {
                DisplayChoose();
                return;
            }
            Thread.Sleep(2000);
            Environment.Exit(0);
        }
    }

    private static void MakeMove()
    {
        var move = Console.ReadLine();

        if (int.TryParse(move, out int moveInt) && moveInt >= 1 && moveInt <= 9 && board[moveInt] == "#")
        {
            client.Send(move);
            GamePage.DisplayTable();
        }
        else
        {
            Console.SetCursorPosition(22, 27);
            Console.WriteLine("                                                                   ");
            Console.SetCursorPosition(22, 27);
            Console.Write("Mossa non valida o cella occupata. Reinserire una mossa: ");
            MakeMove();
        }
    }

    private static void ChooseMode(string message)
    {
        Console.Clear();
        HomePage.WriteHome();

        string mode = HomePage.ChooseMode();

        if (mode != "a" && mode != "b")
        {
            ChooseMode(message);
        }

        HomePage.Gioca();

        client.Send(mode);

        GamePage.DisplayTable();
    }

    private static void PrintBoard(string boardString)
    {
        // Salva la tabella precedente
        _previousBoard = string.Join("", board);

        board.Clear();

        for (int i = 0; i < boardString.Length; i++)
        {
            board.Add(boardString[i].ToString());
            if (i == 0)
            {
                var temp = board[i].Split('*');
                board[i] = temp[1];
            }
        }

        // Controlla se la tabella è stata aggiornata
        var currentBoard = string.Join("", board);
        if (currentBoard != _previousBoard)
        {
            // Se la tabella è stata aggiornata, stampa la nuova tabella con i limiti
            Console.Clear();
            GamePage.DisplayTable(); // Stampa i limiti della tabella
            for (int i = 1; i < board.Count(); i++)
            {
                if (board[i] == "X")
                {
                    int[] coordinates = table[i];
                    GamePage.PrintCross(coordinates[0], coordinates[1]);
                }
                else if (board[i] == "O")
                {
                    int[] coordinates = table[i];
                    GamePage.PrintCircle(coordinates[0], coordinates[1]);
                }
            }
        }
    }

    public static void LoginManager()
    {
        LoginPage.Login();
        string login = Console.ReadLine();

        if (!login.StartsWith("login:") && !login.StartsWith("register:") && !(login == "guest" || login == "Guest"))
        {
            LoginManager();
        }
        client.Send(login);
        //Thread.Sleep(2500);
    }

    private static void DisplayChoose()
    {
        Console.SetCursorPosition(0, 16);
        Console.WriteLine("Vuoi giocare ancora?");

        string frecciaSelezioneDx = "────>";

        Console.SetCursorPosition(31, 18);
        Console.WriteLine("Gioca ancora", Color.Red);

        Console.SetCursorPosition(31, 20);
        Console.Write("ESCI");

        Console.SetCursorPosition(24, 18);
        Console.Write(frecciaSelezioneDx);

        int mode;
        int cont = 0;

        ConsoleKey key;
        do
        {
            key = Console.ReadKey(true).Key;

            if (key == ConsoleKey.UpArrow)
            {
                cont = 0;
            }
            else if (key == ConsoleKey.DownArrow)
            {
                cont = 1;
            }

            if (cont == 0)
            {
                Console.SetCursorPosition(24, 20);
                Console.Write("      ");
                Console.SetCursorPosition(24, 18);
                Console.Write(frecciaSelezioneDx);
                Console.SetCursorPosition(31, 20);
                Console.WriteLine("ESCI", Color.White);
                Console.SetCursorPosition(31, 18);
                Console.WriteLine("Gioca ancora", Color.Red);
                mode = 1;
            }
            else
            {
                Console.SetCursorPosition(24, 18);
                Console.Write("      ");
                Console.SetCursorPosition(24, 20);
                Console.Write(frecciaSelezioneDx);
                Console.SetCursorPosition(31, 18);
                Console.Write("Gioca ancora", Color.White);
                Console.SetCursorPosition(31, 20);
                Console.WriteLine("ESCI", Color.Red);
                mode = 2;
            }
        } while (key != ConsoleKey.Enter);

        if (mode == 1)
        {
            HomePage.Gioca();
            client.Send("nuovaPartita");
            board.Clear();
            board = new() { "#", "#", "#", "#", "#", "#", "#", "#", "#" };
            isGameFinished = false;
            _isPlayingWithBot = default;
        }
        else
        {
            Environment.Exit(0);
        }
    }
}