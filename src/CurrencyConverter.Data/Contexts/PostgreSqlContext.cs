using CurrencyConverter.Data.Configs;
using CurrencyConverter.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Data.Contexts
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options)
            : base(options)
        {

        }

        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CurrencyRate> CurrencyRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .ApplyConfiguration(new CurrencyConfig())
                .ApplyConfiguration(new CurrencyRateConfig());
        }
    }
}
