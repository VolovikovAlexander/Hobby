namespace Leaderboard.Core.Models;

/// <summary>
/// Реализация интерфейса связки пользователя с занятым местом <see cref="IUserWithPlace"/>
/// </summary>
public class UserWithPlace: User, IUserWithPlace
{
    public int Place { get; set; }
}