using Leaderboard.Models;
using Leaderboard.Services;

namespace Leaderboard.Tests;

public class LeaderboardCalculatorTests
{
    private readonly ILeaderboardCalculator leaderboardCalculator;

    public LeaderboardCalculatorTests()
    {
        this.leaderboardCalculator = new LeaderboardCalculator();
    }

    [Fact]
    public void NotEnoughScoreForFirstPlaces()
    {
        var minScores = new LeaderboardMinScores(100, 50, 10);
        var usersWithScores = new List<User>
        {
            new User(userId: 1, score: 3),
            new User(userId: 2, score: 2),
            new User(userId: 3, score: 1)
        };

        var result = this.leaderboardCalculator.CalculatePlaces(usersWithScores, minScores);

        var expectedResult = new List<UserWithPlace>
        {
            new UserWithPlace(userId: 1, place: 4),
            new UserWithPlace(userId: 2, place: 5),
            new UserWithPlace(userId: 3, place: 6)
        };

        Assert.True(CheckResult(result, expectedResult));
    }

    [Fact]
    public void AllScoreForFirstPlaces()
    {
        var minScores = new LeaderboardMinScores(100, 50, 10);
        var usersWithScores = new List<User>
        {
            new User(userId: 1, score: 100),
            new User(userId: 2, score: 50),
            new User(userId: 3, score: 10),
            new User(userId: 4, score: 9),
            new User(userId: 5, score: 8),
            new User(userId: 6, score: 7)
        };

        var result = this.leaderboardCalculator.CalculatePlaces(usersWithScores, minScores);

        var expectedResult = new List<UserWithPlace>
        {
            new UserWithPlace(userId: 1, place: 1),
            new UserWithPlace(userId: 2, place: 2),
            new UserWithPlace(userId: 3, place: 3),
            new UserWithPlace(userId: 4, place: 4),
            new UserWithPlace(userId: 5, place: 5),
            new UserWithPlace(userId: 6, place: 6)
        };

        Assert.True(CheckResult(result, expectedResult));
    }

    [Fact]
    public void OnlyFirstPlaceAndLowScores()
    {
        var minScores = new LeaderboardMinScores(100, 50, 10);
        var usersWithScores = new List<User>
        {
            new User(userId: 1, score: 100),
            new User(userId: 2, score: 3),
            new User(userId: 3, score: 2),
            new User(userId: 4, score: 1)
        };

        var result = this.leaderboardCalculator.CalculatePlaces(usersWithScores, minScores);

        var expectedResult = new List<UserWithPlace>
        {
            new UserWithPlace(userId: 1, place: 1),
            new UserWithPlace(userId: 2, place: 4),
            new UserWithPlace(userId: 3, place: 5),
            new UserWithPlace(userId: 4, place: 6)
        };

        Assert.True(CheckResult(result, expectedResult));
    }

    //[Fact]
    //public void TwoFirstPlaceAndLowScores()
    //{
    //    var minScores = new LeaderboardMinScores(100, 50, 10);
    //    var usersWithScores = new List<User>
    //    {
    //        new User(userId: 1, score: 100),
    //        new User(userId: 2, score: 100),
    //        new User(userId: 3, score: 2),
    //        new User(userId: 4, score: 1)
    //    };

    //    var result = this.leaderboardCalculator.CalculatePlaces(usersWithScores, minScores);

    //    var expectedResult = new List<UserWithPlace>
    //    {
    //        new UserWithPlace(userId: 1, place: 1),
    //        new UserWithPlace(userId: 2, place: 1),
    //        new UserWithPlace(userId: 3, place: 4),
    //        new UserWithPlace(userId: 4, place: 5)
    //    };

    //    Assert.True(CheckResult(result, expectedResult));
    //}

    [Fact]
    public void OnlySecondPlace()
    {
        var minScores = new LeaderboardMinScores(100, 50, 10);
        var usersWithScores = new List<User>
        {
            new User(userId: 1, score: 55)
        };

        var result = this.leaderboardCalculator.CalculatePlaces(usersWithScores, minScores);

        var expectedResult = new List<UserWithPlace>
        {
            new UserWithPlace(userId: 1, place: 2)
        };

        Assert.True(CheckResult(result, expectedResult));
    }

    //[Fact]
    //public void TwoSecondPlace()
    //{
    //    var minScores = new LeaderboardMinScores(100, 50, 10);
    //    var usersWithScores = new List<User>
    //    {
    //        new User(userId: 1, score: 55),
    //        new User(userId: 2, score: 60)
    //    };

    //    var result = this.leaderboardCalculator.CalculatePlaces(usersWithScores, minScores);

    //    var expectedResult = new List<UserWithPlace>
    //    {
    //        new UserWithPlace(userId: 1, place: 2),
    //        new UserWithPlace(userId: 2, place: 2)
    //    };

    //    Assert.True(CheckResult(result, expectedResult));
    //}

    private static bool CheckResult(IEnumerable<UserWithPlace> result, IEnumerable<UserWithPlace> expectedResult)
    {
        if (result == null)
        {
            return false;
        }

        List<UserWithPlace> resultList;
        try
        {
            resultList = result.ToList();
        }
        catch
        {
            return false;
        }

        var expectedResultList = expectedResult.ToList();
        if (resultList.Count != expectedResultList.Count)
        {
            return false;
        }

        foreach (var expectedResultUser in expectedResultList)
        {
            var resultUser = resultList.FirstOrDefault(e => e?.UserId == expectedResultUser.UserId);
            if (resultUser == null || resultUser.Place != expectedResultUser.Place)
            {
                return false;
            }
        }

        return true;
    }
}