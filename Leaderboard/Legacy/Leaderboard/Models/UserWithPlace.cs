using Newtonsoft.Json;

namespace Leaderboard.Models
{
    public class UserWithPlace
    {
        public long UserId { get; private set; }
        public int Place { get; private set; }

        public UserWithPlace(long userId, int place)
        {
            UserId = userId;
            Place = place;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}