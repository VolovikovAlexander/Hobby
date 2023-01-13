namespace Leaderboard.Core;

/// <summary>
/// Интерфейс - описание связки пользователя с набранными очками
/// </summary>
public interface IUserWithScore: IUser
{
   /// <summary>
   /// Количество баллов
   /// </summary>
   public int Score { get;  set; }
}