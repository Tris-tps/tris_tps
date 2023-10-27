using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketTrisServer
{
    internal class Bot
    {
        public static int BotMove(ref char[] board)
        {
            // Il bot Minimax calcola e effettua la sua mossa
            int bestScore = int.MinValue;
            int bestMove = -1;

            for (int i = 0; i < 9; i++)
            {
                if (board[i] != 'X' && board[i] != 'O')
                {
                    char originalValue = board[i];
                    board[i] = 'O'; // Imposta una possibile mossa del bot (simbolo 'O')
                    int score = Minimax(board, 0, false);
                    board[i] = originalValue;

                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = i; // Memorizza la mossa con il punteggio migliore
                    }
                }
            }
            if (bestMove != -1)
            {
                board[bestMove] = 'O'; // Il bot Minimax effettua la sua mossa migliore
            }
            return bestMove;
        }

        static int Minimax(char[] currentBoard, int depth, bool isMaximizing)
        {
            if (isMaximizing)
            {
                int bestScore = int.MinValue;

                for (int i = 0; i < 9; i++)
                {
                    if (currentBoard[i] != 'X' && currentBoard[i] != 'O')
                    {
                        char originalValue = currentBoard[i];
                        currentBoard[i] = 'O'; // Prova una mossa del bot (simbolo 'O')
                        int score = Minimax(currentBoard, depth + 1, false);
                        currentBoard[i] = originalValue;
                        bestScore = Math.Max(score, bestScore); // Memorizza il punteggio migliore
                    }
                }

                return bestScore; // Restituisce il punteggio migliore per il bot
            }
            else
            {
                int bestScore = int.MaxValue;

                for (int i = 0; i < 9; i++)
                {
                    if (currentBoard[i] != 'X' && currentBoard[i] != 'O')
                    {
                        char originalValue = currentBoard[i];
                        currentBoard[i] = 'X'; // Prova una mossa del giocatore (simbolo 'X')
                        int score = Minimax(currentBoard, depth + 1, true);
                        currentBoard[i] = originalValue;
                        bestScore = Math.Min(score, bestScore); // Memorizza il punteggio migliore per il giocatore
                    }
                }

                return bestScore; // Restituisce il punteggio migliore per il giocatore
            }
        }

        static bool IsGameOver(char[] board)
        {
            return board.All(cell => cell == 'X' || cell == 'O');
        }
    }
}