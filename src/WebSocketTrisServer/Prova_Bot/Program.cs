using System;

namespace TrisMinimaxBot
{
    public enum CellState
    {
        Empty,
        X,
        O
    }

    public class Board
    {
        public CellState[] Cells { get; private set; }
        public int Size { get; private set; }

        public Board(int size)
        {
            Size = size;
            Cells = new CellState[size * size];
            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = CellState.Empty;
            }
        }

        public bool IsFull()
        {
            foreach (var cell in Cells)
            {
                if (cell == CellState.Empty)
                    return false;
            }
            return true;
        }

        public bool MakeMove(int position, CellState player)
        {
            if (position < 0 || position >= Size * Size || Cells[position] != CellState.Empty)
            {
                return false;
            }

            Cells[position] = player;
            return true;
        }

        public void UndoMove(int position)
        {
            Cells[position] = CellState.Empty;
        }

        public void Print()
        {
            for (int i = 0; i < Size; i++)
            {
                for (int j = 0; j < Size; j++)
                {
                    var cell = Cells[i * Size + j];
                    Console.Write(cell == CellState.Empty ? "_ " : cell + " ");
                }
                Console.WriteLine();
            }
        }
    }

    public class Minimax
    {
        public static int Evaluate(Board board)
        {
            // ... Valutazione dello stato attuale del tabellone (implementa la tua logica di valutazione)
            // Qui puoi impostare la logica per valutare lo stato attuale del gioco e ritornare un punteggio.
            // Ad esempio, potresti dare punti per linee completate.

            // Questo è solo uno scheletro di una funzione di valutazione.
            return 0;
        }

        public static int MinimaxAlgorithm(Board board, int depth, bool isMaximizingPlayer)
        {
            if (board.IsFull() || depth == 0)
            {
                return Evaluate(board);
            }

            if (isMaximizingPlayer)
            {
                int bestScore = int.MinValue;

                for (int i = 0; i < board.Size * board.Size; i++)
                {
                    if (board.MakeMove(i, CellState.X))
                    {
                        int score = MinimaxAlgorithm(board, depth - 1, false);
                        bestScore = Math.Max(score, bestScore);
                        board.UndoMove(i);
                    }
                }

                return bestScore;
            }
            else
            {
                int bestScore = int.MaxValue;

                for (int i = 0; i < board.Size * board.Size; i++)
                {
                    if (board.MakeMove(i, CellState.O))
                    {
                        int score = MinimaxAlgorithm(board, depth - 1, true);
                        bestScore = Math.Min(score, bestScore);
                        board.UndoMove(i);
                    }
                }

                return bestScore;
            }
        }

        public static int FindBestMove(Board board)
        {
            int bestScore = int.MinValue;
            int bestMove = -1;

            for (int i = 0; i < board.Size * board.Size; i++)
            {
                if (board.MakeMove(i, CellState.X))
                {
                    int score = MinimaxAlgorithm(board, int.MaxValue, false);
                    board.UndoMove(i);

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i;
                    }
                }
            }

            return bestMove;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Board board = new Board(3);
            bool playerXTurn = true;

            while (true)
            {
                board.Print();

                if (playerXTurn)
                {
                    Console.WriteLine("Player X's turn. Enter position (0-8): ");
                    int position = int.Parse(Console.ReadLine());

                    if (board.MakeMove(position, CellState.X))
                    {
                        playerXTurn = false;
                    }
                    else
                    {
                        Console.WriteLine("Invalid move. Try again.");
                    }
                }
                else
                {
                    Console.WriteLine("Bot O's turn.");

                    int bestMove = Minimax.FindBestMove(board);

                    if (bestMove != -1)
                    {
                        board.MakeMove(bestMove, CellState.O);
                        playerXTurn = true;
                    }
                    else
                    {
                        Console.WriteLine("No valid moves left. It's a draw!");
                        break;
                    }
                }

                // Check for win or draw conditions
                // Implement your win/draw logic here...

                // For simplicity, the game continues indefinitely until interrupted.
            }
        }
    }
}
