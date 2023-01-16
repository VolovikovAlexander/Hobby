using System.Reflection;
using Leaderboard.Core.Models;

namespace Leaderboard.Core;

public static class UserWithScoreExtension
{
    /// <summary>
    /// Получить словарь с настройками (место / значение баллов)
    /// </summary>
    /// <param name="source"> Набор исходных данных </param>
    /// <returns></returns>
    public static IDictionary<LeaderboardPlace, int> GetRanks(this ILeaderboardMinScores source)
       =>  source.GetType().GetProperties().Select(x => new
            {
                Value = (int)x.GetValue(source)!, Rank = x.GetCustomAttribute<LeaderboardAttribute>() ?? throw new InvalidOperationException($"Поле {x.Name} не имеет связанного атрибута типа LeaderboardAttribute!")
            }).ToDictionary(x => x.Rank.Place, x => x.Value);


    /// <summary>
    /// Определить ранг для указанного значения
    /// </summary>
    /// <param name="settings"> Настройки </param>
    /// <param name="value"> Значение </param>
    /// <returns></returns>
    private static LeaderboardPlace BuildRank(IDictionary<LeaderboardPlace, int> settings, int value)
    {
        ArgumentNullException.ThrowIfNull(settings);
        
        // Значение выходит за границы
        if(settings.Values.All(x => x < value))
            return LeaderboardPlace.None;
    
        // Место, если значение меньше    
        return settings
            .OrderBy(x => x.Key)
            .First(x => x.Value >= value)
            .Key;
    } 
    
    /// <summary>
    /// Провести ранжирование пользователей согласно настройкам <see cref="ILeaderboardMinScores"/>
    /// </summary>
    /// <param name="source"> Исходные данные </param>
    /// <param name="settings"> Настройки </param>
    /// <returns></returns>
    public static IEnumerable<IUserWithPlace> BuildRank(this IEnumerable<IUserWithScore> source,
        ILeaderboardMinScores settings)
    {
        ArgumentNullException.ThrowIfNull(source);
        ArgumentNullException.ThrowIfNull(settings);
        if (!source.Any())
            throw new ArgumentException("Некорректно переданы параметры!", nameof(source));

        var ranks = settings.GetRanks();
        
        // Получаем список с рейтингом для всех записей
        var items
            = source.Select(x => new UserWithPlace()
            {
                UserId = x.UserId,
                Place = BuildRank(ranks, x.Score)
            }).Where(x => x.Place != LeaderboardPlace.None)
                .ToList();

        var result = items.GroupBy(x => x.Place)
            .Select(x => new
            {
                // Место в рейтинге
                Place = x.Key,
                // Пользователь (тот, у кого нужный рейтинг и максимальное количество баллов)
                UserId = (from s in items
                          from p in source
                          where s.UserId.Equals(p.UserId) && s.Place.Equals(x.Key)
                          orderby p.Score 
                          select s.UserId)
                    .FirstOrDefault()
            })
            .Select(x => new UserWithPlace()
            {
                UserId = x.UserId, Place = x.Place
            });

        return result;
    }
}