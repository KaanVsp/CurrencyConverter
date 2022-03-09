using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Data.Contexts
{
    public class PostgreSqlContext : DbContext
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options)
            : base(options)
        {

        }
    }
}
