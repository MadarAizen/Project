using System;
using System.Linq;
using TicTacToeProj.EF;
using TicTacToeProj.Enum;

namespace TicTacToeProj.Models
{
    public abstract class BaseGame
    {
        public BaseGame(GameContext db)
        {
            _db = db;
            field = new int[SIZE, SIZE];
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    field[i, j] = -1;
                }
            }
        }

        protected GameContext _db;

        protected const int SIZE = 3;

        protected int[,] field;

        public int Id { get; set; }

        public GameType GameType { get; protected set; }

        public abstract void SaveResuts(WonNumber wonNumber);

        protected void PrintField()
        {
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (field[i, j] == 0)
                    {
                        Console.Write("0 ");
                    }
                    else if (field[i, j] == 1)
                    {
                        Console.Write("X ");
                    }
                    else if (field[i, j] == -1)
                    {
                        Console.Write("  ");
                    }
                    if (j + 1 < SIZE)
                    {
                        Console.Write("|");
                    }
                }
                if (i + 1 < SIZE)
                {
                    Console.WriteLine();
                    Console.WriteLine("--+--+--");
                }
            }
        }

        protected int CheckIfWon()
        {
            bool win;
            //
            for (int i = 0; i < SIZE; i++)
            {
                win = true;
                for (int j = 0; j < SIZE; j++)
                {
                    if (field[j, i] != 0)
                    {
                        win = false;
                    }
                }
                if (win)
                {
                    return 0;
                }
            }

            for (int i = 0; i < SIZE; i++)
            {
                win = true;
                for (int j = 0; j < SIZE; j++)
                {
                    if (field[i, j] != 0)
                    {
                        win = false;
                    }
                }
                if (win)
                {
                    return 0;
                }
            }
            //
            for (int i = 0; i < SIZE; i++)
            {
                win = true;
                for (int j = 0; j < SIZE; j++)
                {
                    if (field[i, j] != 1)
                    {
                        win = false;
                    }
                }
                if (win)
                {
                    return 1;
                }
            }
            //
            for (int i = 0; i < SIZE; i++)
            {
                win = true;
                for (int j = 0; j < SIZE; j++)
                {
                    if (field[j, i] != 1)
                    {
                        win = false;
                    }
                }
                if (win)
                {
                    return 1;
                }
            }
            win = true;
            for (int i = 0; i < SIZE; i++)
            {
                if (field[i, i] != 0)
                {
                    win = false;
                }
            }
            if (win)
            {
                return 0;
            }
            win = true;
            for (int i = 0; i < SIZE; i++)
            {
                if (field[i, i] != 1)
                {
                    win = false;
                }
            }
            if (win)
            {
                return 1;
            }
            if (field[0, 2] == 0 && field[1, 1] == 0 && field[2, 0] == 0)
            {
                return 0;
            }
            if (field[0, 2] == 1 && field[1, 1] == 1 && field[2, 0] == 1)
            {
                return 1;
            }
            win = true;
            for (int i = 0; i < SIZE; i++)
            {
                for (int j = 0; j < SIZE; j++)
                {
                    if (field[i, j] == -1)
                    {
                        win = false;
                    }
                }
            }
            if (win)
            {
                return 3;
            }
            return -1;
        }

        protected void Move(bool isCross, int row, int cols)
        {
            if ((row <= 0 || row > SIZE) || (cols <= 0 || cols > SIZE))
            {
                throw new Exception("Bound error exception");
            }
            switch (field[row - 1, cols - 1])
            {
                case 0:
                    {
                        throw new Exception("Zero has already placed here");
                    }
                case 1:
                    {
                        throw new Exception("Cross has already placed hear");
                    }
                case -1:
                    {
                        if (isCross)
                        {
                            field[row - 1, cols - 1] = 1;
                        }
                        else
                        {
                            field[row - 1, cols - 1] = 0;
                        }
                        break;
                    }
            }
        }

        public abstract void StartGame();
    }
}
