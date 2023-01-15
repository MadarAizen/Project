using Microsoft.EntityFrameworkCore;
using TicTacToeProj.Models;

namespace TicTacToeProj.EF
{
    public class GameContext : DbContext
    {
        public GameContext()
        {
            Database.EnsureCreated();
        }

        public DbSet<Player> Players { get; set; }

        public DbSet<GameResult> GameResults { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=lrgame.db");
        }
    }
}
