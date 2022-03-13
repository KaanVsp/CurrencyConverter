using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyConverter.Domain.Entities
{
    [Table("Currency")]
    public class Currency : EntityBase
    {
        public string Symbol { get; set; }
        public virtual List<CurrencyRate> Rates { get; set; } = new List<CurrencyRate>();

        public Currency()
        {

        }

        public Currency Clone()
        {
            return new Currency()
            {
                InsertTime = this.InsertTime,
                Symbol = this.Symbol,
                Rates = new List<CurrencyRate>()
            };
        }
    }
}
