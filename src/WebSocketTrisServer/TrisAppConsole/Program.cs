﻿using Colorful;
using Console = Colorful.Console;
using System.Drawing;

namespace ClientView
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(40, 30);
            Console.Title = "TrisApp";
            Console.CursorVisible = false;

            LoginPage.WriteLogo();
            
            string clientUsername = "";
            clientUsername =  LoginPage.Login();

            Console.Clear();

            HomePage.WriteHome();

            int i = 0;
            i = HomePage.ChooseMode();

            if (i>0)
            {
                HomePage.Gioca();
                Console.Clear();
            }
           

            Console.WriteLine("Premere un tasto qualsiasi per uscire ...");
            Console.ReadKey(true);
        }
    }
}
