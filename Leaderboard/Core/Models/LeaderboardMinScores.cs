namespace Leaderboard.Core.Models;

/// <summary>
/// Реализация настроек для определения рейтинга пользователя
/// </summary>
public class LeaderboardMinScores: ILeaderboardMinScores
{
    /// <summary>
    /// Количество баллов для первого места
    /// </summary>
    public int FirstPlaceMinScore { get; set; }
    
    /// <summary>
    /// Количество баллов для второго места
    /// </summary>
    public int SecondPlaceMinScore { get; set; }
    
    /// <summary>
    /// Количество баллов для третьего места
    /// </summary>
    public int ThirdPlaceMinScore { get; set; }
}