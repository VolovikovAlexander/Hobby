using Leaderboard.Models;

namespace Leaderboard.Services
{
    public interface ILeaderboardCalculator
    {
        IEnumerable<UserWithPlace> CalculatePlaces(IEnumerable<IUserWithScore> usersWithScores, LeaderboardMinScores leaderboardMinScores);
    }
}
