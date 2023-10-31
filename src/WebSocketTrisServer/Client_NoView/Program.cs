using WebSocketSharp;

namespace Client_NoView;

public class Program
{
    private static List<string> board = new List<string>();
    private static WebSocket _client;

    private static void MakeMove()
    {
        var move = Console.ReadLine();

        if (int.TryParse(move, out int moveInt) && moveInt >= 1 && moveInt <= 9)
            _client.Send(move);
        else
        {
            Console.WriteLine("Mossa non valida. Inserisci un numero tra 1 e 9");
            MakeMove();
        }
    }

    private static void ChooseMode(string message)
    {
        Console.WriteLine(message);
        var mode = Console.ReadLine();

        if (mode != "a" && mode != "b")
        {
            ChooseMode(message);
        }
        _client.Send(mode);
    }

    private static void PrintBoard(string boardString)
    {
        board.Clear();
        for (int i = 0; i < boardString.Length; i++)
        {
            board.Add(boardString[i].ToString());
            if (i == 0)
            {
                var temp = board[i].Split('*');
                board[i] = temp[1];
            }
            if (i > 0 && i % 3 == 0)
            {
                board[i] += "\n";
            }
        }

        for (int i = 0; i < board.Count; i++)
        {
            Console.Write(board[i]);
        }
    }

    private static void LoginManager()
    {
        Console.WriteLine("Inserisci 'login:username' per effettuare il login.");
        Console.WriteLine("Inserisci 'register:username' per registrarti.");

        var login = Console.ReadLine();
        if (!login.StartsWith("login:") && !login.StartsWith("register:"))
        {
            LoginManager();
        }
        _client.Send(login);
    }

    static void Main(string[] args)
    {
        Thread threadWhileTrue = new Thread(() =>
        {
            while (true) { }
        });
        threadWhileTrue.Start();
        Console.WriteLine("client_2");
        _client = new WebSocket("ws://127.0.0.1:5000");
        Thread.Sleep(100);
        _client.Connect();
        _client.OnMessage += Message;
    }

    private static void Message(object? obj, MessageEventArgs e)
    {
        var data = e.Data;
        if (!(data[0] == '*') && !(data == "+") && data[0] != '?' && data != "login") //il ' * ' è utilizzato per identificare che il dato sia la board
        {
            Console.WriteLine(data);
        }
        else if (data == "+")
        {
            MakeMove();
        }
        else if (data[0] == '?')
        {
            var var = data.Split('?');
            ChooseMode(var[1] + var[2]);
        }
        else if (data == "login")
        {
            LoginManager();
        }
        else if (data[0] == '*')
        {
            PrintBoard(data);
        }
    }
}