using Leaderboard.Core.Models;

namespace Leaderboard.Core;

/// <summary>
/// Построитель. Создает корректный вариант настроек объекта типа <see cref="ILeaderboardMinScores"/>
/// </summary>
public static class LeaderboardMinScoresBuilder
{
    /// <summary>
    /// Создать объект настроек типа <see cref="ILeaderboardMinScores"/>
    /// </summary>
    /// <param name="first"> Значение баллов для первого места </param>
    /// <param name="second"> Значение баллов для второго места </param>
    /// <param name="third"> Значение баллов для третьего места </param>
    /// <returns></returns>
    public static ILeaderboardMinScores Create(int first, int second, int third)
    {
        if (first <= 0 || second <= 0 || third <= 0)
            throw new ArgumentException("Значение баллов должно быть больше нуля!");
        
        var items = new HashSet<int>() { first, second, third };
        if (items.Count() != 3)
            throw new ArgumentException("Значение баллов должно быть разным для каждого места!");

        return new LeaderboardMinScores()
            { FirstPlaceMinScore = first, SecondPlaceMinScore = second, ThirdPlaceMinScore = third };
    }
}