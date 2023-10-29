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
            Console.Title = "TrisApp";
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
           
            GamePage.DisplayTable();

            Console.SetCursorPosition(0, 26);
            Console.WriteLine("Premere un tasto qualsiasi per uscire ...");
            Console.ReadKey(true);
        }
    }
}
