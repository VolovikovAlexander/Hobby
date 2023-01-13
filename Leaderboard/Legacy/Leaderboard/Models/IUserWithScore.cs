namespace Leaderboard.Models
{
    public interface IUserWithScore
    {
        public long UserId { get; }
        public int Score { get; }
    }
}
