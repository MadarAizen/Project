using Microsoft.EntityFrameworkCore;
using System;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.Serialization;
using TicTacToeProj.EF;
using TicTacToeProj.Enum;

namespace TicTacToeProj.Models
{
    public class GameWithPlayers: BaseGame
    {
        public GameWithPlayers(GameContext db, string firstPlayer, string secondPlayer): base(db)
        {
            GameType = GameType.WithPlayer;
            Login(firstPlayer, secondPlayer);
        }
        public Player FirstPlayer { get; private set; }

        public Player SecondPlayer { get; private set; }

        private void Login(string firstPlayer, string secondPlayer)
        {
            var firstAccount = _db.Players.Include(x => x.GameResults).FirstOrDefault(x => x.Name == firstPlayer);
            var secondAccount = _db.Players.Include(x => x.GameResults).FirstOrDefault(x => x.Name == secondPlayer); 
            if (firstAccount == null)
            {
                _db.Players.Add(new Player(firstPlayer));
                _db.SaveChanges();
                FirstPlayer = _db.Players.Include(x => x.GameResults).FirstOrDefault(x => x.Name == firstPlayer);
            }
            else
            {
                FirstPlayer = firstAccount;
            }
            if (secondAccount == null)
            {
                _db.Players.Add(new Player(secondPlayer));
                _db.SaveChanges();
                SecondPlayer = _db.Players.Include(x => x.GameResults).FirstOrDefault(x => x.Name == secondPlayer);
            }
            else
            {
                SecondPlayer = secondAccount;
            }
        }

        public override void SaveResuts(WonNumber wonNumber)
        {
            GameResult firstResult, secondResult;
            switch (wonNumber)
            {
                case WonNumber.First:
                    {
                        firstResult = new GameResult(GameType, GameEnd.Win);
                        secondResult = new GameResult(GameType, GameEnd.Lose);
                        FirstPlayer.GameResults.Add(firstResult);
                        SecondPlayer.GameResults.Add(secondResult);
                        _db.SaveChanges();
                        break;
                    }
                case WonNumber.Second:
                    {
                        firstResult = new GameResult(GameType, GameEnd.Lose);
                        secondResult = new GameResult(GameType, GameEnd.Win);
                        FirstPlayer.GameResults.Add(firstResult);
                        SecondPlayer.GameResults.Add(secondResult);
                        _db.SaveChanges();
                        break;
                    }
                case WonNumber.Draw:
                    {
                        firstResult = new GameResult(GameType, GameEnd.Draw);
                        secondResult = new GameResult(GameType, GameEnd.Draw);
                        SecondPlayer.GameResults.Add(firstResult);
                        FirstPlayer.GameResults.Add(secondResult);
                        _db.SaveChanges();
                        break;
                    }
            }
        }
        public override void StartGame()
        {
            int turn = 1, col, row, gameResult;
            Console.WriteLine($"{1} гравець: {FirstPlayer}Вiн грає за хрестики\n\n{2} Гравець: {SecondPlayer}Вiн грає за нулi");
            while(true)
            {
                Console.WriteLine($"\n{turn} гравець. Ваш хiд\nIгрове поле:\n");
                PrintField();
                Console.Write("\nВведiть номер рядка, де треба зробити хiд: ");
                row = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.Write("Введiть номер стовпця, де треба зробити хiд: ");
                col = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                switch (turn)
                {
                    case 1:
                        {
                            Move(true, row, col);
                            break;
                        }
                    case 2:
                        {
                            Move(false, row, col);
                            break;
                        }
                }
                Console.WriteLine("Додано!");
                gameResult = CheckIfWon();
                switch (gameResult)
                {
                    case 1:
                        {
                            Console.WriteLine($"{1} гравець виграв!!");
                            SaveResuts(WonNumber.First);
                            return;
                        }
                    case 0:
                        {
                            Console.WriteLine($"{2} гравець виграв!!");
                            SaveResuts(WonNumber.Second);
                            return;
                        }
                    case 3:
                        {
                            Console.WriteLine("Нiчия!");
                            SaveResuts(WonNumber.Draw);
                            break;
                        }
                }
                if (turn == 2)
                {
                    turn = 1;
                }
                else
                {
                    turn++;
                }
            }
        }
    }
}
