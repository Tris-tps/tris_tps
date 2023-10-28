using Colorful;
using Console = Colorful.Console;

namespace ClientView
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.SetWindowSize(40, 30);
            Console.Title = "TrisApp";
            LoginPage.WriteLogo();
            
            Console.SetCursorPosition(33, 10);
            string clientUsername = "";
            clientUsername =  LoginPage.Login();

            Console.Clear();

            Console.WriteLine(clientUsername);

            Console.WriteLine("Premere un tasto qualsiasi per uscire ...");
            Console.ReadKey(true);
        }
    }
}