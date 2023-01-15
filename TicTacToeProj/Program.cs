using System;
using TicTacToeProj.Models;

namespace TicTacToeProj
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GameWithPlayers game = new GameWithPlayers(new EF.GameContext(), "user", "user1");
            game.StartGame();
            GameWithBot game2 = new GameWithBot(new EF.GameContext(), "user");
            game2.StartGame();
        }
    }
}
