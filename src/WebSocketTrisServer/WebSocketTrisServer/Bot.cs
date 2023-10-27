//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace WebSocketTrisServer
//{
//    internal class Bot
//    {
//        public static string[] board;

//        public Bot(string[] boardProgram) 
//        {
//            board = boardProgram;

//        }

//        public static void BotMove()
//        {
//            // Implementazione dell'algoritmo Minimax
//            int result = GetWinner();

//            if (result == 1)
//                return -1; // Il giocatore 1 (X) vince
//            else if (result == 2)
//                return 1; // Il giocatore 2 (O) vince
//            else if (IsGameOver())
//                return 0; // La partita è finita in pareggio

//            if (isMaximizing)
//            {
//                int bestScore = int.MinValue;

//                for (int i = 0; i < 9; i++)
//                {
//                    if (currentBoard[i] != 'X' && currentBoard[i] != 'O')
//                    {
//                        char originalValue = currentBoard[i];
//                        currentBoard[i] = 'O'; // Prova una mossa del bot (simbolo 'O')
//                        int score = Minimax(currentBoard, depth + 1, false);
//                        currentBoard[i] = originalValue;
//                        bestScore = Math.Max(score, bestScore); // Memorizza il punteggio migliore
//                    }
//                }

//                return bestScore; // Restituisce il punteggio migliore per il bot
//            }
//            else
//            {
//                int bestScore = int.MaxValue;

//                for (int i = 0; i < 9; i++)
//                {
//                    if (currentBoard[i] != 'X' && currentBoard[i] != 'O')
//                    {
//                        char originalValue = currentBoard[i];
//                        currentBoard[i] = 'X'; // Prova una mossa del giocatore (simbolo 'X')
//                        int score = Minimax(currentBoard, depth + 1, true);
//                        currentBoard[i] = originalValue;
//                        bestScore = Math.Min(score, bestScore); // Memorizza il punteggio migliore per il giocatore
//                    }
//                }

//                return bestScore; // Restituisce il punteggio migliore per il giocatore
//            }
//        }
//    }
//}