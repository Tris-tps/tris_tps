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
    private static bool isLoginFinished = false;
    public static Dictionary<int, int[]> table = new Dictionary<int, int[]>();
    private static string _previousBoard = string.Empty;

    private static void MakeMove()
    {
        var move = Console.ReadLine();

        GamePage.DisplayTable(); // Stampa la tabella corrente

        if (int.TryParse(move, out int moveInt) && moveInt >= 1 && moveInt <= 9)
            client.Send(move);
        else
        {
            Console.WriteLine("Mossa non valida. Inserisci un numero tra 1 e 9");
            MakeMove();
        }

        // Dopo l'invio della mossa, la tabella corrente viene memorizzata come tabella precedente
        //PrintBoard(); // Stampa la nuova tabella con la mossa effettuata
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
            // Se la tabella è stata aggiornata, stampa la nuova tabella
            Console.Clear();
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

    /*
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
            // Se la tabella è stata aggiornata, stampa la nuova tabella
            Console.Clear();
            for (int i = 0; i < board.Count(); i++)
            {
                if (board[i] == "X")
                {
                    int[] coordinates = table[i+1];
                    GamePage.PrintCross(coordinates[0], coordinates[1]);
                }
                else if (board[i] == "O")
                {
                    int[] coordinates = table[i+1];
                    GamePage.PrintCircle(coordinates[0], coordinates[1]);
                }
            }
        }
    }
    */

    /*
    private static void PrintBoard(string boardString)
    {
        board.Clear();

        for (int i = 0; i < boardString.Length; i++)
        {
            board.Add(boardString[i].ToString());
            if (i == 0)
            {
                var temp = board[i].Split('*');
                board[i] = temp[1];
            }
            //if (i > 0 && i % 3 == 0)
            //{
            //    board[i] += "\n";
            //}
        }

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
    */

    public static void LoginManager()
    {
        LoginPage.Login();
        string login = Console.ReadLine();

        if (!login.StartsWith("login:") && !login.StartsWith("register:"))
        {
            LoginManager();
        }
        client.Send(login);
        isLoginFinished = true;
        Thread.Sleep(1000);
    }

    static void Main(string[] args)
    {
        Thread threadWhileTrue = new Thread(() =>
        {
            while (true) { }
        });
        threadWhileTrue.Start();
        Console.SetWindowSize(40, 30);
        Console.Title = "TrisApp";
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
        if (!(data[0] == '*') && !(data == "+") && data[0] != '?' && data != "login") //il ' * ' è utilizzato per identificare che il dato sia la board
        {
            if (!isLoginFinished)
            {
                Console.SetCursorPosition(28, 21);
                Console.WriteLine("                              ");
                Console.SetCursorPosition(28, 21);
                Console.WriteLine(data);
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
        else if (data[0] == '*')
        {
            PrintBoard(data);
        }
    }
}