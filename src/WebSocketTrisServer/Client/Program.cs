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
        private static string myCurrentID = "";
        static void Main(string[] args)
        {
            Console.WriteLine("client");
            WebSocket client = new WebSocket("ws://127.0.0.1:5000");
            client.Connect();
            client.OnMessage += Message;
            while (true) 
            {
                //if (currentPlayerID == "YourClientID") // Sostituisci "YourClientID" con l'ID del tuo client
                //{
                //    Console.Write("Inserisci la tua mossa (1-9): ");
                //    string move = Console.ReadLine();
                //    client.Send(move);
                //}
            };
        }

        static void Message(object? obj, MessageEventArgs e)
        {
            Console.WriteLine(e.Data);
        }

        //static void Open(object? obj, EventArgs e) 
        //{
        //    Console.WriteLine(e.ToString());
        //}
    }
}