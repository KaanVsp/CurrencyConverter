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

        public async Task AddNewRates(List<Currency> Currencies, string BaseSymbol)
        {
            var existingCurrencies = _context.Currencies.ToList();

            if (existingCurrencies.Count == 0)
            {
                existingCurrencies = this.CreateInitialCurrencies(Currencies);
            }

            this.AddChilds(existingCurrencies, Currencies, BaseSymbol);

            _context.SaveChanges();
        }

        private List<Currency> CreateInitialCurrencies(List<Currency> Currencies)
        {
            List<Currency> onlyParents = new List<Currency>();

            foreach (var currency in Currencies)
            {
                onlyParents.Add(currency.Clone());
            }

            _context.Currencies.AddRange(onlyParents);
            _context.SaveChanges();

            return onlyParents;
        }

        private void AddChilds(List<Currency> ExistingCurrencies, List<Currency> NewRates, string BaseSymbol)
        {
            int? baseCurrencyId = ExistingCurrencies.FirstOrDefault(x => x.Symbol == BaseSymbol)?.Id;

            if (baseCurrencyId.HasValue)
            {
                NewRates.ForEach(x => x.Rates.ForEach(a => a.SecondCurrencyId = baseCurrencyId.Value));

                foreach (var newCurrency in NewRates)
                {
                    if (ExistingCurrencies.Any(x => NewRates.Select(a => a.Symbol).Contains(x.Symbol)))
                    {
                        var existingCurrency = ExistingCurrencies.Where(x => x.Symbol == newCurrency.Symbol).First();
                        existingCurrency.Rates.Add(newCurrency.Rates.First());
                    }
                    else
                    {
                        _context.Currencies.Add(newCurrency);
                    }
                }

                _context.SaveChanges();
            }
        }
    }
}
