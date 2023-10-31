using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Colorful;
using Console = Colorful.Console;
using System.Drawing;
using System.Xml.Schema;

namespace Client
{
    public class GamePage
    {
        public static void PrintCircle(int left, int top) //O
        {
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@"   ____  ", Color.Blue);
                }   
                else if (i == 1)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@"  / __ \ ", Color.Blue);
                }
                else if (i == 2)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@" | |  | |", Color.Blue);
                }             
                else if (i == 3)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@" | |  | |", Color.Blue);
                }      
                else if (i == 4)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@" | |__| |", Color.Blue);
                }
                else if (i == 5)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@"  \____/ ", Color.Blue);
                }
            }
        }
        public static void PrintCross(int left, int top) //X
        {
            for (int i = 0; i < 6; i++)
            {
                if (i == 0)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@" __   __", Color.Red);
                }
                else if (i == 1)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@" \ \ / /", Color.Red);
                }
                else if (i == 2)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@"  \ V / ", Color.Red);
                }
                else if (i == 3)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@"   > <  ", Color.Red);
                }
                else if (i == 4)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@"  / . \ ", Color.Red);
                }
                else if (i == 5)
                {
                    Console.SetCursorPosition(left, top + i);
                    Console.Write(@" /_/ \_\", Color.Red);
                }
            }
        }
        public static void DisplayTable()
        {
            string tableDoubleLines = @"  
                                                    ║       ║      
                                                X   ║   O   ║   X  
                                                    ║       ║      
                                             ═══════╬═══════╬═══════
                                                    ║       ║      
                                                X   ║   O   ║   X  
                                                    ║       ║      
                                             ═══════╬═══════╬═══════
                                                    ║       ║      
                                                X   ║   O   ║   X  
                                                    ║       ║       
";
            string tableSingleLine = @"
                                                    │       │      
                                                X   │   O   │   X  
                                                    │       │      
                                             ───────┼───────┼───────
                                                    │       │      
                                                X   │   O   │   X  
                                                    │       │      
                                             ───────┼───────┼───────
                                                    │       │      
                                                X   │   O   │   X  
                                                    │       │       
";
            string tableBig = @"
                                                    |       |      
                                                    |       |      
                                                    |       |      
                                             -----------------------
                                                    |       |      
                                                    |       |      
                                                    |       |      
                                             -----------------------
                                                    |       |      
                                                    |       |      
                                                    |       |       
";
            string tableSmall = @"
                                                    |   |  
                                                 -----------
                                                    |   |  
                                                 -----------
                                                    |   |   
 
";


            string tableBigDoubleLines = @"
                                  
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                     ═══════════════╬═══════════════╬═══════════════
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                     ═══════════════╬═══════════════╬═══════════════
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║              
                                    ║               ║               
";
            Console.WriteLine(tableBigDoubleLines);

            //PrintCircle(24, 2); //casella in alto a sinistra (1)
            //PrintCross(24, 10); //casella in mezzo a sinistra (4)
            //PrintCircle(24, 18); //casella in basso a sinistra (7)

            //PrintCross(40, 2); //casella in alto al centro (2)
            //PrintCircle(40, 10); //casella in mezzo al centro (5)
            //PrintCross(40, 18); //casella in basso al centro (8)

            //PrintCircle(56, 2); //casella in alto a destra (3)
            //PrintCross(56, 10); //casella in mezzo a destra (5)
            //PrintCircle(56, 18); //casella in basso a destra (9)


            //Console.SetCursorPosition(0, 26);
            //Console.WriteLine("1 for win, 2 for draw, 3 for lose, >3 for gays");
            //int i = int.Parse(Console.ReadLine());
            //return i;
        }
    }
}
