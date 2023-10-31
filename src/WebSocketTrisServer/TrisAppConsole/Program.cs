using Colorful;
using Console = Colorful.Console;
using System.Drawing;

namespace ClientView
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(40, 30);
            Console.Title = "Client_1";
            Console.CursorVisible = false;

            Console.OutputEncoding = System.Text.Encoding.UTF8;

            LoginPage.WriteLogo();
            string clientUsername = "";
            clientUsername = LoginPage.Login();

            Console.Clear();

            HomePage.WriteHome();

            int i = 0;
            i = HomePage.ChooseMode();

            if (i>0)
            {
                HomePage.Gioca();
                Console.Clear();
            }

            int r = 0; 
            r = GamePage.DisplayTable();

            Console.Clear();

            if (r == 1)
            {
                ResultsPage.DisplayWin();
            }
            else if (r == 2)
            {
                ResultsPage.DisplayDraw();
            }
            else if (r == 3)
            {
                ResultsPage.DisplayLose();
            }
            else 
            {
                Console.WriteLine("finocchio");
            }

            Console.SetCursorPosition(0, 23);
            Console.WriteLine("Premere un tasto qualsiasi per uscire ...");
            Console.ReadKey(true);
        }
    }
}
