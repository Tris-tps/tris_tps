using System.Net.Sockets;
using System.Net;
using System.Text.Json;
using WebSocketTrisServer;
using System.IO;
using System;
using WebSocketSharp;
using System.Linq.Expressions;

namespace Client
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("client");
            WebSocket client = new WebSocket("ws://127.0.0.1:5000");
            client.Connect();
            client.OnMessage += Message;
            client.Send("ciao 5iB");
            while (true) { };
        }

        static void Message(object? obj, MessageEventArgs e)
        {

        }
    }
}