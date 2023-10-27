using Colorful;
using Console = Colorful.Console;

namespace TrisAppConsole
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "TrisApp";
            LoginPage.WriteLogo();
            
            Console.SetCursorPosition(33, 10);
            LoginPage.Login();


            Console.ReadKey(true);
        }
    }
}