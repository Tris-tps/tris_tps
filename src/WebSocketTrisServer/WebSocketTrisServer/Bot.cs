﻿using System;
using System.ComponentModel;
using System.Numerics;

namespace WebSocketTrisServer
{
    public class Bot
    {
        //public static char[,] ChangeState(char[] b)
        //{
        //    char[,] matrix = new char[3, 3];
        //    int index = 0;

        //    for (int i = 0; i < 3; i++)
        //    {
        //        for (int j = 0; j < 3; j++)
        //        {
        //            matrix[i, j] = b[index];
        //            index++;
        //        }
        //    }
        //    return matrix;
        //}

        public static int BotMove(char[] board)
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
}
