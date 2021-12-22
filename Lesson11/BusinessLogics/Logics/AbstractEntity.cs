
namespace Lesson11.BL
{
    /// <summary>
    /// Реализация абстрактного класса для общей сущности
    /// </summary>
    public abstract class AbstractEntity : IEntity
    {
        protected long _id = 0;
        public long Id { get => _id; set => _id = value; }
    }
}
