using Leaderboard.Core;

namespace Test;

public class Common
{
    [SetUp]
    public void Setup()
    {
    }

    /// <summary>
    /// Проверить построитель объектов типа <see cref="IUserWithScore"/>
    /// </summary>
    [Test]
    [TestCase(100)]
    public void Check_Builder_UserWithScore(int size)
    {
        // Подготовка
        
        // Действие
        var result = UserWithScoreBuilder.Create(size);
        
        // Проверки
        Assert.That(result.Count(), Is.EqualTo(size));
        Assert.IsTrue(result.Any(x => x.Score > 0));
    }

    /// <summary>
    /// Проверить построить объекта типа <see cref="ILeaderboardMinScores"/>
    /// </summary>
    [Test]
    public void Check_Builder_LeaderboardMinScores()
    {
        // Подготовка
        
        // Действие
        var result = LeaderboardMinScoresBuilder.Create(1, 2, 3);
        
        // Проверки
        Assert.IsTrue(result.FirstPlaceMinScore == 1 && result.SecondPlaceMinScore == 2 && result.ThirdPlaceMinScore == 3);
    }

    /// <summary>
    /// Проверить работу расширения <see cref="ILeaderboardMinScores"/>
    /// </summary>
    [Test]
    public void Check_Extension_GetSettings()
    {
        // Подготовка
        var settings =  LeaderboardMinScoresBuilder.Create(1, 2, 3);

        // Действие    
        var result = (settings as ILeaderboardMinScores).GetRanks();
        
        // Проверки
        Assert.IsTrue(result.Any());
    }
}