namespace Leaderboard.Core.Models;

/// <summary>
/// Реализация настроек для определения рейтинга пользователя
/// </summary>
public class LeaderboardMinScores: ILeaderboardMinScores
{
    /// <summary>
    /// Количество баллов для первого места
    /// </summary>
    [Leaderboard(LeaderboardPlace.First)]
    public int FirstPlaceMinScore { get; set; }
    
    /// <summary>
    /// Количество баллов для второго места
    /// </summary>
    [Leaderboard(LeaderboardPlace.Second)]
    public int SecondPlaceMinScore { get; set; }
    
    /// <summary>
    /// Количество баллов для третьего места
    /// </summary>
    [Leaderboard(LeaderboardPlace.Third)]
    public int ThirdPlaceMinScore { get; set; }
}