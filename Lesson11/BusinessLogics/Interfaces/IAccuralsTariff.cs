
namespace Lesson11.BL
{
    /// <summary>
    /// Тариф для процесса начисления
    /// </summary>
    public interface IAccuralsTariff: IEntity
    {
        /// <summary>
        /// Ставка начисления
        /// </summary>
        public double Cost { get; set; }
        
        /// <summary>
        /// Наименование
        /// </summary>
        public string Description { get; set; }
    }
}
