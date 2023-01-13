namespace Leaderboard.Core.Models;

/// <summary>
/// Реализация интерфейса связки пользователя с количеством набранных баллов <see cref="IUserWithScore"/>
/// </summary>
public class UserWithScore: User, IUserWithScore
{
    public int Score { get; set; }
}