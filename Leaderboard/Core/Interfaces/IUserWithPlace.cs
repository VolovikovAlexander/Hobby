namespace Leaderboard.Core;

/// <summary>
/// Интерфейс - описание связки пользователя с его местом (рейтингом)
/// </summary>
public interface IUserWithPlace: IUser
{
    /// <summary>
    /// Место в рейтинги по баллам
    /// </summary>
    public LeaderboardPlace Place { get;  set; }
}