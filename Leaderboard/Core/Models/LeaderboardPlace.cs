namespace Leaderboard.Core;

/// <summary>
/// Тип занятого места
/// </summary>
public enum LeaderboardPlace: int
{
    /// <summary>
    /// Первое местое
    /// </summary>
    First = 1,
    
    /// <summary>
    /// Второе месте
    /// </summary>
    Second = 2,
    
    /// <summary>
    /// Третье место
    /// </summary>
    Third = 3,
    
    /// <summary>
    /// -
    /// </summary>
    None = 9999
}