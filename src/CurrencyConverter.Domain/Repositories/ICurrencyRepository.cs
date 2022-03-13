using CurrencyConverter.Domain.Entities;

namespace CurrencyConverter.Domain.Repositories
{
    public interface ICurrencyRepository
    {
        Task<List<Currency>> GetAll();
        Task<Currency> GetById(int Id);
        Task AddNewRates(List<Currency> Currencies, string BaseSymbol);
        Task UpdateRate(int CurrencyId, double NewRate);
        Task<List<Currency>> GetCurrencies(int FirstCurrencyId, int SecondCurrencyId);
    }
}
