using System;
using System.Linq;

namespace TicTacToe
{
    class TicTacToeGame
    {
        public static char[] board = Enumerable.Repeat('#', 9).ToArray();

        public static void PrintBoard()
        {
            Console.WriteLine($"{board[0]} | {board[1]} | {board[2]}");
            Console.WriteLine("--+---+--");
            Console.WriteLine($"{board[3]} | {board[4]} | {board[5]}");
            Console.WriteLine("--+---+--");
            Console.WriteLine($"{board[6]} | {board[7]} | {board[8]}");
        }

        public static int BotMove()
        {
            int bestScore = int.MinValue;
            int move = -1;

            for (int i = 0; i < 9; i++)
            {
                if (board[i] == '#')
                {
                    board[i] = 'O';
                    int score = MiniMax(board, 0, false);
                    board[i] = '#';

                    if (score > bestScore)
                    {
                        bestScore = score;
                        move = i;
                    }
                }
            }
            return move;
        }

        static int MiniMax(char[] board, int depth, bool isMaximizing)
        {
            char human = 'X';
            char ai = 'O';

            if (CheckWin(board, ai))
                return 10;
            else if (CheckWin(board, human))
                return -10;
            else if (CheckDraw(board))
                return 0;

            if (isMaximizing)
            {
                int bestScore = int.MinValue;
                for (int i = 0; i < 9; i++)
                {
                    if (board[i] == '#')
                    {
                        board[i] = ai;
                        bestScore = Math.Max(bestScore, MiniMax(board, depth + 1, false));
                        board[i] = '#';
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;
                for (int i = 0; i < 9; i++)
                {
                    if (board[i] == '#')
                    {
                        board[i] = human;
                        bestScore = Math.Min(bestScore, MiniMax(board, depth + 1, true));
                        board[i] = '#';
                    }
                }
                return bestScore;
            }
        }

        public static bool CheckWin(char[] b, char player)
        {
            return (b[0] == player && b[1] == player && b[2] == player) ||
                   (b[3] == player && b[4] == player && b[5] == player) ||
                   (b[6] == player && b[7] == player && b[8] == player) ||
                   (b[0] == player && b[3] == player && b[6] == player) ||
                   (b[1] == player && b[4] == player && b[7] == player) ||
                   (b[2] == player && b[5] == player && b[8] == player) ||
                   (b[0] == player && b[4] == player && b[8] == player) ||
                   (b[2] == player && b[4] == player && b[6] == player);
        }

        public static bool CheckDraw(char[] b)
        {
            return !b.Contains('#');
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            TicTacToeGame.PrintBoard();
            while (true)
            {
                Console.Write("Inserisci la tua mossa (da 1 a 9): ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int move) && move >= 1 && move <= 9 && TicTacToeGame.board[move - 1] == '#')
                {
                    TicTacToeGame.board[move - 1] = 'X';

                    //TicTacToeGame.PrintBoard();

                    if (TicTacToeGame.CheckWin(TicTacToeGame.board, 'X'))
                    {
                        Console.WriteLine("Hai vinto!");
                        break;
                    }
                    else if (TicTacToeGame.CheckDraw(TicTacToeGame.board))
                    {
                        Console.WriteLine("La partita è finita in pareggio.");
                        break;
                    }

                    int botMove = TicTacToeGame.BotMove();
                    TicTacToeGame.board[botMove] = 'O';

                    TicTacToeGame.PrintBoard();

                    if (TicTacToeGame.CheckWin(TicTacToeGame.board, 'O'))
                    {
                        Console.WriteLine("Il bot ha vinto!");
                        break;
                    }
                    else if (TicTacToeGame.CheckDraw(TicTacToeGame.board))
                    {
                        Console.WriteLine("La partita è finita in pareggio.");
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Mossa non valida, riprova.");
                }
            }
        }
    }
}
