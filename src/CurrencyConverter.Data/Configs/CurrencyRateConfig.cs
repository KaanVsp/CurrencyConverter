using CurrencyConverter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyConverter.Data.Configs
{
    internal class CurrencyRateConfig : IEntityTypeConfiguration<CurrencyRate>
    {
        public void Configure(EntityTypeBuilder<CurrencyRate> builder)
        {
            builder.HasQueryFilter(x => x.DeleteTime == null);
        }
    }
}
