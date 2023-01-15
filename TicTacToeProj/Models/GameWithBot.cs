using System.Drawing;
using System;
using TicTacToeProj.EF;
using TicTacToeProj.Enum;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;

namespace TicTacToeProj.Models
{
    public class GameWithBot: BaseGame
    {
        public GameWithBot(GameContext db, string firstPlayer) : base(db)
        {
            GameType = GameType.WithBot;
            Login(firstPlayer);
        }
        public Player FirstPlayer { get; private set; }

        private void Login(string firstPlayer)
        {
            var firstAccount = _db.Players.Include(x => x.GameResults).FirstOrDefault(x => x.Name == firstPlayer);
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
        }

        public override void SaveResuts(WonNumber wonNumber)
        {
            switch (wonNumber)
            {
                case WonNumber.First:
                    {
                        FirstPlayer.GameResults.Add(new GameResult(GameType, GameEnd.Win));
                        _db.SaveChanges();
                        break;
                    }
                case WonNumber.Second:
                    {
                        FirstPlayer.GameResults.Add(new GameResult(GameType, GameEnd.Lose));
                        _db.SaveChanges();
                        break;
                    }
                case WonNumber.Draw:
                    {
                        FirstPlayer.GameResults.Add(new GameResult(GameType, GameEnd.Draw));
                        _db.SaveChanges();
                        break;
                    }
            }
        }
        public override void StartGame()
        {
            int turn = 1, col, row, gameResult;
            Console.WriteLine($"Гравець: {FirstPlayer}Вiн грає за хрестики\nБот грає за нулi");
            while (true)
            {
                Console.WriteLine($"\nГравець. Ваш хiд\nIгрове поле:\n");
                PrintField();
                Console.Write("\nВведiть номер рядка, де треба зробити хiд: ");
                row = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Console.Write("Введiть номер стовпця, де треба зробити хiд: ");
                col = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine();
                Move(true, row, col);
                Console.WriteLine("Додано!");
                gameResult = CheckIfWon();
                switch (gameResult)
                {
                    case 1:
                        {
                            Console.WriteLine($"{1} гравець виграв!!");
                            SaveResuts(WonNumber.First);
                            break;
                        }
                    case 0:
                        {
                            Console.WriteLine($"Бот виграв!!");
                            SaveResuts(WonNumber.Second);
                            break;
                        }
                    case 3:
                        {
                            Console.WriteLine("Нiчия!");
                            break;
                        }
                }
                bool flag = false;
                for (int i = 0; i < SIZE; i++)
                {
                    for (int j = 0; j < SIZE; j++)
                    {
                        if (field[i,j] == -1)
                        {
                            Move(false, i+1, j+1);
                            flag = true;
                            break;
                        }
                    }
                    if(flag)
                    {
                        break;
                    }
                }
                Console.WriteLine("Бот Зробив хiд!");
                PrintField();
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
                            Console.WriteLine($"Бот виграв!!");
                            SaveResuts(WonNumber.Second);
                            return;
                        }
                    case 3:
                        {
                            Console.WriteLine("Нiчия!");
                            SaveResuts(WonNumber.Draw);
                            return;
                        }
                }
            }
        }
    }
}
