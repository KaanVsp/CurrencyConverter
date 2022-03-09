using CurrencyConverter.Data.Contexts;
using CurrencyConverter.Domain.Entities;
using CurrencyConverter.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CurrencyConverter.Data.Repositories
{
    public class CurrencyRepository : ICurrencyRepository
    {
        private readonly PostgreSqlContext _context;

        public CurrencyRepository(PostgreSqlContext context)
        {
            this._context = context;
        }

        public async Task<List<Currency>> GetAll()
        {
            return await _context.Currencies
                .ToListAsync();
        }

        public async Task<Currency> GetById(int Id)
        {
            return await _context.Currencies
                .FirstOrDefaultAsync(x => x.Id == Id);
        }
    }
}
