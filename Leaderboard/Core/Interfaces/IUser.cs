namespace Leaderboard.Core;

/// <summary>
/// Интерфейс - описание пользователя
/// </summary>
public interface IUser
{
    /// <summary>
    /// Уникальный код пользователя
    /// </summary>
    public long UserId { get;  set; }
}