using TicTacToeProj.Enum;

namespace TicTacToeProj.Models
{
    public class GameResult
    {
        public GameResult(GameType gameType, GameEnd gameEnd)
        {
            GameType = gameType;
            GameEnd = gameEnd;
        }

        public int Id { get; set; }

        public GameType GameType { get; set; }

        public GameEnd GameEnd { get; set; }

        public override string ToString()
        {
            return $"Game Result:\nGame type: {GameType}. Game end: {GameEnd}";
        }
    }
}
