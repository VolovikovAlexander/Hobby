using Leaderboard.Core.Models;

namespace Leaderboard.Core;

/// <summary>
/// Построитель. Создает нужное количество пользователей с очками с разными вариантами
/// </summary>
public static class UserWithScoreBuilder
{
    private static IEnumerable<IUserWithScore> _users = Enumerable.Empty<IUserWithScore>();
    private static readonly Random _rnd = new Random();

    /// <summary>
    /// Сформировать необходимое количество пользователей с произвольными баллами
    /// </summary>
    /// <param name="size"> Количество пользователей </param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public static IEnumerable<IUserWithScore> Create(int size)
    {
        if (size < 4)
            throw new ArgumentException("Некорректно указано колличество пользователей! Должно быть больше 3-ех");

        return Enumerable.Range(0, size)
            .Select(x => new UserWithScore()
            {
                UserId = _rnd.NextInt64(9999), Score = _rnd.Next(9999)
            });
    }
}