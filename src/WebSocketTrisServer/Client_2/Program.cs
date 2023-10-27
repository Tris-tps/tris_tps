using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using WebSocketTrisServer;
using System.IO;
using System;
using WebSocketSharp;
using System.Linq.Expressions;

namespace Client_2;
public class Program
{
    private static string myCurrentID = "";
    private static List<string> board = new List<string>();
    public static WebSocket client;

    public static void MakeMove()
    {
        var move = Console.ReadLine();
        client.Send(move);
    }

    public static void PrintBoard(string boardString)
    {
        List<string> board = new List<string>();

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

    static void Main(string[] args)
    {
        Thread threadWhileTrue = new Thread(() =>
        {
            while (true) { }
        });
        threadWhileTrue.Start();
        Console.WriteLine("client_2");
        client = new WebSocket("ws://127.0.0.1:5000");
        Thread.Sleep(100);
        client.Connect();
        client.OnMessage += Message;
    }

    static void Message(object? obj, MessageEventArgs e)
    {
        var data = e.Data;
        if (!(data[0] == '*') && !(data == "+")) //il '*' è utilizzato per identificare che il dato sia la board
        {
            Console.WriteLine(data);
        }
        else if (data == "+")
        {
            MakeMove();
        }
        else
        {
            PrintBoard(data);
        }
    }
}