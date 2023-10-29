using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ClientView
{
    public class HomePage
    {
        public static void WriteHome()
        {
            Console.SetCursorPosition(15, 0);
            string homeLogo = @"
                     ____  ____   ___   ____    ____  ________  
                    |_   ||   _|.'   `.|_   \  /   _||_   __  | 
                      | |__| | /  .-.  \ |   \/   |    | |_ \_| 
                      |  __  | | |   | | | |\  /| |    |  _| _  
                     _| |  | |_\  `-'  /_| |_\/_| |_  _| |__/ | 
                    |____||____|`.___.'|_____||_____||________| 
                                ";

            Console.WriteLine(homeLogo);
        }

        public static int ChooseMode()
        {
            Console.SetCursorPosition(12, 9);
            string settingsPrint = @"
                                 |    |   _)                     
                      __|   _ \  __|  __|  |  __ \    _` |   __| 
                    \__ \   __/  |    |    |  |   |  (   | \__ \ 
                    ____/ \___| \__| \__| _| _|  _| \__, | ____/ 
                                                    |___/        
                                    ";
            Console.WriteLine(settingsPrint);

            Console.SetCursorPosition(0, 16);
            Console.WriteLine("Scegliere la difficoltà. Premi invio su quella che desideri muovendondoti su e giù");

            string frecciaSelezioneDx = "────>";
            string frecciaSelezioneSx = "<────";

            Console.SetCursorPosition(31, 18);
            Console.WriteLine("UTENTE vs UTENTE");

            Console.SetCursorPosition(31, 20);
            Console.Write("UTENTE vs BOT");

            Console.SetCursorPosition(24, 18);
            Console.Write(frecciaSelezioneDx);

            int mode = 0;
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
                    Console.WriteLine("UTENTE vs BOT", Color.White);
                    Console.SetCursorPosition(31, 18);
                    Console.Write("                ");
                    Console.SetCursorPosition(31, 18);
                    Console.WriteLine("UTENTE vs UTENTE", Color.Red);
                    mode = 1;
                }
                else
                {
                    Console.SetCursorPosition(24, 18);
                    Console.Write("      ");
                    Console.SetCursorPosition(24, 20);
                    Console.Write(frecciaSelezioneDx);
                    Console.SetCursorPosition(31, 18);
                    Console.Write("UTENTE vs UTENTE", Color.White);
                    Console.SetCursorPosition(31, 20);
                    Console.WriteLine("UTENTE vs BOT", Color.Red);
                    mode = 2;
                }
            } while (key != ConsoleKey.Enter);

            Console.SetCursorPosition(0, 25);

            return mode;
        }

        public static void Gioca()
        {
            Console.OutputEncoding = Encoding.UTF8;

            Console.SetCursorPosition(29, 21);
            string gioca = @"
                   ╭────────────────────────────────────────╮
                   │  ██████  ██  ██████   ██████  █████    │
                   │  ██       ██ ██    ██ ██      ██   ██  │
                   │  ██   ███ ██ ██    ██ ██      ███████  │
                   │  ██    ██ ██ ██    ██ ██      ██   ██  │
                   │  ██    ██ ██ ██    ██ ██      ██   ██  │
                   │   ██████  ██  ██████   ██████ ██   ██  │
                   ╰────────────────────────────────────────╯
                                     ";
            Console.Write(gioca);

            ConsoleKey key;
            do
            {
                key = Console.ReadKey(true).Key;
                Console.SetCursorPosition(37, 25);
            } while (key != ConsoleKey.Enter);

        }
    }
}
