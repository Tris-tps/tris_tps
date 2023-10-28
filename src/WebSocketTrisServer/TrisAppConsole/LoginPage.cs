﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;

namespace ClientView
{
    public class LoginPage
    {
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

            Console.WriteLine(logo2, Color.Red);
        }

        public static string Login()
        {
            string loginText = @"
                                |              _)       
                                |   _ \   _` |  |    \  
                               _| \___/ \__, | _| _| _| 
                                        ____/           ";
            Console.OutputEncoding = Encoding.UTF8;
            
            Console.WriteLine(loginText, Color.White);

            string loginUsernameBorder = @"
                                   ╭───────────────╮
                       USERNAME =  │               │
                                   ╰───────────────╯
                                           ";

            Console.WriteLine(loginUsernameBorder);


            Console.SetCursorPosition(37, 17);

            
            string username = "";
            int cont = 0;
            ConsoleKeyInfo collegamento;
            do
            {
                collegamento = Console.ReadKey(true);
                username = Console.ReadLine();
                //controllo sullo username
                cont++;

                if (cont==1)
                {
                    Console.SetCursorPosition(22, 21);
                    Console.WriteLine("Username Corretto. Attendi Per accedere");
                    Thread.Sleep(2000);
                    break;
                }
                else
                {
                    Console.SetCursorPosition(24, 20);
                    Console.WriteLine("Username Sbagliato. Reinserire le credenziali");
                    Console.SetCursorPosition(37, 17);
                }
            } while (collegamento.Key != ConsoleKey.Enter);

            return username;

        }

    }
}
