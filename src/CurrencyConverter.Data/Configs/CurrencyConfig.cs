using CurrencyConverter.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CurrencyConverter.Data.Configs
{
    internal class CurrencyConfig : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasQueryFilter(x => x.DeleteTime == null);

            builder.Property(x => x.Symbol).HasMaxLength(3).IsFixedLength(true);

            builder.HasMany<CurrencyRate>(x => x.Rates)
                .WithOne(x => x.FirstCurrency)
                .HasForeignKey(x => x.FirstCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
