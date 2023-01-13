using Newtonsoft.Json;

namespace Leaderboard.Models
{
    public class LeaderboardMinScores
    {
        public int FirstPlaceMinScore { get; private set; }
        public int SecondPlaceMinScore { get; private set; }
        public int ThirdPlaceMinScore { get; private set; }

        public LeaderboardMinScores(int firstPlaceMinScore, int secondPlaceMinScore, int thirdPlaceMinScore)
        {
            FirstPlaceMinScore = firstPlaceMinScore;
            SecondPlaceMinScore = secondPlaceMinScore;
            ThirdPlaceMinScore = thirdPlaceMinScore;
        }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}