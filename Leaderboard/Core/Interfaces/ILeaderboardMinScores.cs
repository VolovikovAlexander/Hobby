namespace Leaderboard.Core;

/// <summary>
/// Интерфейс для настройки позиций по баллам среди лидеров
/// </summary>
public interface ILeaderboardMinScores
{
    /// <summary>
    /// Первое место
    /// </summary>
    public int FirstPlaceMinScore { get;  set; }
    
    /// <summary>
    /// Второе место
    /// </summary>
    public int SecondPlaceMinScore { get;  set; }
    
    /// <summary>
    /// Третье место
    /// </summary>
    public int ThirdPlaceMinScore { get;  set; }
}