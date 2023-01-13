namespace Leaderboard.Core.Models;

/// <summary>
/// Реализация интерфейса описания пользователя <see cref="IUser"/>
/// </summary>
public class User: IUser
{
    public long UserId { get; set; }
}