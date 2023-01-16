namespace Leaderboard.Core.Models;

/// <summary>
/// Класс атрибут для связывания настройки (количество баллов) с местом
/// </summary>
public class LeaderboardAttribute : Attribute
{
    private LeaderboardPlace _place;

    /// <summary>
    /// Место
    /// </summary>
    public LeaderboardPlace Place => _place;

    public LeaderboardAttribute(LeaderboardPlace place)
    {
        _place = place;
    }
}