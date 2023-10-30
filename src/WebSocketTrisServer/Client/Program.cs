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
namespace Client;

public class Program
{
    private static List<string> board = new List<string>();
    public static WebSocket client;

    private static void MakeMove()
    {
        var move = Console.ReadLine();

        if (int.TryParse(move, out int moveInt) && moveInt >= 1 && moveInt <= 9)
            client.Send(move);
        else
        {
            Console.WriteLine("Mossa non valida. Inserisci un numero tra 1 e 9");
            MakeMove();
        }
    }

    private static void ChooseMode(string message)
    {
        Console.WriteLine(message);
        var mode = Console.ReadLine();

        if (mode != "a" && mode != "b")
        {
            ChooseMode(message);
        }
        client.Send(mode);
    }

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
            if (i > 0 && i % 3 == 0)
            {
                board[i] += "\n";
            }
        }

        for (int i = 0; i < board.Count; i++)
        {
            Console.Write(board[i]);
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
    }

    private static void Message(object? obj, MessageEventArgs e)
    {
        var data = e.Data;
        if (!(data[0] == '*') && !(data == "+") && data[0] != '?' && data != "login") //il ' * ' è utilizzato per identificare che il dato sia la board
        {
            Console.WriteLine(data);
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