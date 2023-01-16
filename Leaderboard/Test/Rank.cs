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
    [TestCase(10)]
    [TestCase(100)]
    [TestCase(1000)]
    [TestCase(10000)]
    public void Check_BuildRanks_Variant_1(int size)
    {
        // Подготовка
        var settings = LeaderboardMinScoresBuilder.Create(1000, 2000, 3000);
        var users = UserWithScoreBuilder.Create(10, settings);
        
        // Действие
        var result = users.BuildRank(settings).ToList();
        
        // Проверки
        Console.WriteLine(string.Join("\n", result.OrderBy(x => x.Place).Select(x => $"UserId = {x.UserId}, Place = {x.Place}")));
        Assert.IsTrue(result.Any());
    }
}