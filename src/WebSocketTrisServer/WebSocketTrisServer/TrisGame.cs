using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebSocketTrisServer
{
    public class TrisGame
    {
        public GameStatuses GameState = GameStatuses.Waiting;
        public int[] Board = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0};
        public int CurrentPlayer { get; set; }
        //public Player Player
    }

    public enum GameStatuses
    {
        Waiting,
        Playing,
        Draw,
        Win
    }
}
