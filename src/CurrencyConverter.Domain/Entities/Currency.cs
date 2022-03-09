using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CurrencyConverter.Domain.Entities
{
    [Table("Currency")]
    public class Currency : EntityBase
    {
        public string Symbol { get; set; }
        public virtual List<CurrencyRate> Rates { get; set; } = new List<CurrencyRate>();
    }
}
