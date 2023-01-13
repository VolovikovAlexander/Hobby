using Newtonsoft.Json;

namespace Leaderboard.Models
{
    public class User : IUserWithScore
    {
        public long UserId { get; private set; }
        public int Score { get; private set; }

        public User(long userId, int score)
        {
            UserId = userId;
            Score = score;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}