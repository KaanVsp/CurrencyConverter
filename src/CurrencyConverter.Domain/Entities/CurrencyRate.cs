using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyConverter.Domain.Entities
{
    [Table("CurrencyRate")]
    public class CurrencyRate : EntityBase
    {
        public int FirstCurrencyId { get; set; }
        public virtual Currency FirstCurrency { get; set; }
        public int SecondCurrencyId { get; set; }
        public virtual Currency SecondCurrency { get; set; }
        public double Rate { get; set; }

        public CurrencyRate(DateTime InsertTime, double Rate)
        {
            this.InsertTime = InsertTime;
            this.Rate = Rate;
        }
    }
}
