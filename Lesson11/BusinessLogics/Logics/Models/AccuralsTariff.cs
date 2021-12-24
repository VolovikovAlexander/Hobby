namespace Lesson11.BL
{
    public class AccuralsTariff : IAccuralsTariff
    {
        #region IAccuralsTariff
        public double Cost { get; set; }
        public string Description { get; set; }
        public long Id { get; set; }

        #endregion
    }
}
