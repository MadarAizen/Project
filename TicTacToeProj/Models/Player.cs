using System.Collections.Generic;
using TicTacToeProj.Models;

namespace TicTacToeProj.Models
{
    public class Player
    {
        public Player(string name)
        {
            Name = name;
            GameResults = new List<GameResult>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public List<GameResult> GameResults { get; set; }

        public override string ToString()
        {
            string res = $"Id: {Id}. Name: {Name}\nGame Results:\n";
            foreach (var item in GameResults)
            {
                res += item + "\n";
            }
            return res;
        }
    }
}
