using Colorful;
using Console = Colorful.Console;
using System.Drawing;

namespace ProvaPanz
{
    public class ResultsPage
    {
        public static string Vincita()
        {
            string s1 = @"  _    _            _____      __      __ _____  _   _  _______  ____      _ 
 | |  | |    /\    |_   _|     \ \    / /|_   _|| \ | ||__   __|/ __ \    | |
 | |__| |   /  \     | |        \ \  / /   | |  |  \| |   | |  | |  | |   | |
 |  __  |  / /\ \    | |         \ \/ /    | |  | . ` |   | |  | |  | |   | |
 | |  | | / ____ \  _| |_         \  /    _| |_ | |\  |   | |  | |__| |   |_|
 |_|  |_|/_/    \_\|_____|         \/    |_____||_| \_|   |_|   \____/    (_)
                                                                             
                                                                             
                    ";
            return s1;
        }

        public static string Perdita()
        {
            string s1 = @"  _    _            _____       _____   ______  _____    _____   ____      _ 
 | |  | |    /\    |_   _|     |  __ \ |  ____||  __ \  / ____| / __ \    | |
 | |__| |   /  \     | |       | |__) || |__   | |__) || (___  | |  | |   | |
 |  __  |  / /\ \    | |       |  ___/ |  __|  |  _  /  \___ \ | |  | |   | |
 | |  | | / ____ \  _| |_      | |     | |____ | | \ \  ____) || |__| |   |_|
 |_|  |_|/_/    \_\|_____|     |_|     |______||_|  \_\|_____/  \____/    (_)
                                                                             
                                                                             
                    ";
            return s1;
        }

        public static string Pareggio()
        {
            string s1 = @"  _    _            _____       _____          _____   ______  _____   _____  _____         _______  ____      _ 
 | |  | |    /\    |_   _|     |  __ \  /\    |  __ \ |  ____|/ ____| / ____||_   _|    /\ |__   __|/ __ \    | |
 | |__| |   /  \     | |       | |__) |/  \   | |__) || |__  | |  __ | |  __   | |     /  \   | |  | |  | |   | |
 |  __  |  / /\ \    | |       |  ___// /\ \  |  _  / |  __| | | |_ || | |_ |  | |    / /\ \  | |  | |  | |   | |
 | |  | | / ____ \  _| |_      | |   / ____ \ | | \ \ | |____| |__| || |__| | _| |_  / ____ \ | |  | |__| |   |_|
 |_|  |_|/_/    \_\|_____|     |_|  /_/    \_\|_|  \_\|______|\_____| \_____||_____|/_/    \_\|_|   \____/    (_)
                                                                                                                 
                                                                                                                 
                    ";
            return s1;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string vittoria = ResultsPage.Vincita();
            string sconfitta = ResultsPage.Perdita();
            string pareggio = ResultsPage.Pareggio();
            Console.WriteLine(vittoria);
            Console.WriteLine(sconfitta);
            Console.WriteLine(pareggio);
        }
    }
}