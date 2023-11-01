﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;
using WebSocketTrisServer;

namespace Client
{
    public class LoginPage
    {
        //public static string zucche = "";
        //public static int[] posizione = new int[2];

        public static void WriteLogo()
        {
            string logo = @" _________   _____     ______            _________        _          ______            _________     ___     ________  
|  _   _  | |_   _|  .' ___  |          |  _   _  |      / \       .' ___  |          |  _   _  |  .'   `.  |_   __  | 
|_/ | | \_|   | |   / .'   \_|  ______  |_/ | | \_|     / _ \     / .'   \_|  ______  |_/ | | \_| /  .-.  \   | |_ \_| 
    | |       | |   | |        |______|     | |        / ___ \    | |        |______|     | |     | |   | |   |  _| _  
   _| |_     _| |_  \ `.___.'\             _| |_     _/ /   \ \_  \ `.___.'\             _| |_    \  `-'  /  _| |__/ | 
  |_____|   |_____|  `.____ .'            |_____|   |____| |____|  `.____ .'            |_____|    `.___.'  |________| 
                                                                                                                       ";

            string logo2 = @"████████╗██╗ ██████╗      ████████╗ █████╗  ██████╗      ████████╗ ██████╗ ███████╗
╚══██╔══╝██║██╔════╝      ╚══██╔══╝██╔══██╗██╔════╝      ╚══██╔══╝██╔═══██╗██╔════╝
   ██║   ██║██║     █████╗   ██║   ███████║██║     █████╗   ██║   ██║   ██║█████╗  
   ██║   ██║██║     ╚════╝   ██║   ██╔══██║██║     ╚════╝   ██║   ██║   ██║██╔══╝  
   ██║   ██║╚██████╗         ██║   ██║  ██║╚██████╗         ██║   ╚██████╔╝███████╗
   ╚═╝   ╚═╝ ╚═════╝         ╚═╝   ╚═╝  ╚═╝ ╚═════╝         ╚═╝    ╚═════╝ ╚══════╝
                                                                                   ";

            Console.WriteLine(logo2, Color.Orange);
        }

        //public static void ClearCurrentConsoleLine()
        //{
        //    int currentLineCursor = Console.CursorTop;
        //    Console.SetCursorPosition(0, 8);
        //    Console.Write(new string(' ', Console.WindowWidth));
        //    Console.SetCursorPosition(0, currentLineCursor);
        //}

        //public static void ThreadZucche()
        //{
        //    while (true)
        //    {
        //        zucche = zucche + "\U0001F383";
        //        Thread.Sleep(500);
        //        if (zucche.Length >= 12)
        //        {
        //            zucche = "";
        //            ClearCurrentConsoleLine();
        //        }
        //        posizione[0] = Console.CursorLeft;
        //        posizione[1] = Console.CursorTop;
        //        Console.SetCursorPosition(40, 8);
        //        Console.Write(zucche);
        //        Console.SetCursorPosition(posizione[0], posizione[1]);
        //    }
        //}

        public static void Login()
        {
            //Thread tZucche = new Thread(ThreadZucche); // Passa il riferimento del metodo, senza chiamare il metodo
            //tZucche.Start();


            Console.SetCursorPosition(33, 10);
            string loginText = @"
                                |              _)       
                                |   _ \   _` |  |    \  
                               _| \___/ \__, | _| _| _| 
                                        ____/           ";
            Console.OutputEncoding = Encoding.UTF8;


            Console.WriteLine(loginText, Color.White);

            string loginUsernameBorder = @"
                           ╭──────────────────────────────────╮
               USERNAME =  │                                  │
                           ╰──────────────────────────────────╯
                                           ";

            Console.SetCursorPosition(0, 15);

            Console.Write(loginUsernameBorder);

            Console.SetCursorPosition(17, 20);
            Console.WriteLine("Inserisci 'guest' per giocare come ospite.");
            Console.SetCursorPosition(17, 22);
            Console.WriteLine("Inserisci 'login:username' per effettuare il login.");
            Console.SetCursorPosition(17, 24);
            Console.WriteLine("Inserisci 'register:username' per registrarti.");

            Console.SetCursorPosition(29, 17);

            //string login = Console.ReadLine();
            //if (!login.StartsWith("login:") && !login.StartsWith("register:"))
            //{
            //    Console.SetCursorPosition(22, 21);
            //    Console.WriteLine(message);
            //    Console.SetCursorPosition(29, 17);
            //    Console.Write("                                ");
            //    Console.SetCursorPosition(29, 17);
            //    Program.LoginManager(message);
            //}
            //Program.client.Send(login);
            //Console.SetCursorPosition(22, 21);
            //Console.WriteLine("                                                 ");
            //Console.SetCursorPosition(22, 21);
            //Console.WriteLine(message);
            //Thread.Sleep(2000);
            //Console.SetCursorPosition(29, 17);
        }
    }
}
