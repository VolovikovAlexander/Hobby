using Leaderboard.Core;

namespace Test;

public class Rank
{
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Проверить ранжирование пользователей (первый вариант)
    /// </summary>
    [Test]
    public void Check_BuildRanks_Variant_1()
    {
        // Подготовка
        var settings = LeaderboardMinScoresBuilder.Create(1000, 2000, 3000);
        var users = UserWithScoreBuilder.Create(100);
        
        // Действие
        var result = users.BuildRank(settings);
        
        // Проверки
        Assert.IsTrue(result.Any());
    }
}