using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using WebSocketTrisServer;
using System.IO;
using System;
using WebSocketSharp;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Client;

public class Program
{
    private static List<string> board = new List<string>();
    public static WebSocket client;
    public static Dictionary<int, int[]> table = new Dictionary<int, int[]>();
    private static string _previousBoard = string.Empty;
    private static bool isGameFinished  = false;

    private static void MakeMove()
    {
        var move = Console.ReadLine();

        if (int.TryParse(move, out int moveInt) && moveInt >= 1 && moveInt <= 9)
        {
            client.Send(move);
            GamePage.DisplayTable();
        }
        else
        {
            Console.SetCursorPosition(22, 27);
            Console.WriteLine("                                                                   ");
            Console.SetCursorPosition(22, 27);
            Console.Write("Mossa non valida. Inserisci un numero tra 1 e 9: ");
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

        if (!login.StartsWith("login:") && !login.StartsWith("register:"))
        {
            LoginManager();
        }
        client.Send(login);
        Thread.Sleep(2000);
    }

    static void Main(string[] args)
    {
        Thread threadWhileTrue = new Thread(() =>
        {
            while (true) { }
        });
        threadWhileTrue.Start();
        Console.SetWindowSize(40, 30);
        Console.Title = "ClientView_1";
        Console.CursorVisible = false;
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        LoginPage.WriteLogo();
        client = new WebSocket("ws://127.0.0.1:5000");
        Thread.Sleep(100);
        client.Connect();
        client.OnMessage += Message;

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
        if (data[0] != '*' && data != "+" && data[0] != '?' && data != "login"
            && data!= "Hai Vinto!" && data != "Hai Perso!" && data != "La partita è finita in pareggio") //il ' * ' è utilizzato per identificare che il dato sia la board
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
        else if (data == "+")
        {
            MakeMove();
        }
        else if (data[0] == '?')
        {
            var var = data.Split('?');
            ChooseMode(var[1] + var[2]);
        }
        else if (data == "login")
        {
            LoginManager();
        }
        else if (data[0] == '*' && !isGameFinished)
        {
            PrintBoard(data);
        }
        else if(data == "Hai Vinto!")
        {
            Console.Clear();
            ResultsPage.DisplayWin();
            isGameFinished = !isGameFinished;
        }
        else if (data == "Hai Perso!")
        { 
            Console.Clear();
            ResultsPage.DisplayLose();
            isGameFinished = !isGameFinished;
        }
        else if (data == "La partita è finita in pareggio")
        {
            Console.Clear();
            ResultsPage.DisplayDraw();
            isGameFinished = !isGameFinished;
        }
    }


    /*
    private static void Message(object? obj, MessageEventArgs e)
    {
        var data = e.Data;
        Console.SetCursorPosition(28, 27);
        Console.WriteLine("                                            ");
        Console.SetCursorPosition(28, 27);

        switch (data)
        {
            case "È il tuo turno, digita la tua mossa!: ":
                Console.Write(data);
                break;

            case "+":
                MakeMove();
                break;

            case string s when s.StartsWith('?'):
                var parts = s.Split('?');
                ChooseMode(parts[1] + parts[2]);
                break;

            case "login":
                LoginManager();
                break;

            case string s when s.StartsWith('*'):
                PrintBoard(s);
                break;

            case "Hai Vinto!":
                Console.Clear();
                ResultsPage.DisplayWin();
                break;

            case "Hai Perso!":
                Console.Clear();
                ResultsPage.DisplayLose();
                break;

            case "La partita è finita in pareggio":
                Console.Clear();
                ResultsPage.DisplayDraw();
                break;

            default:
                if (!(data[0] == '*') && !(data == "+") && data[0] != '?' && data != "login")
                {
                    if (data != "+")
                    {
                        Console.WriteLine(data);
                    }
                }
                break;
        }
           
    }
    */

}