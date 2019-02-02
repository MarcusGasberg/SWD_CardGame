using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWD_CardGame.Core;

namespace CardGame
{
    class Program
    {
        static void Main(string[] args)
        {
            IGame game = new LowScoreGame(3);
            game.AddPlayer(new NormalPlayer("Jens"));
            game.AddPlayer(new NormalPlayer("Dorthe"));
            game.AddPlayer(new NormalPlayer("Finn"));
            game.AddPlayer(new WeakPlayer("Karl"));
            game.StartNewGame();
            game.PrintPlayerCards();
            game.AnnounceWinner();
            Console.ReadKey();
        }
    }
}
