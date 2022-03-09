using CurrencyConverter.Domain.Entities;

namespace CurrencyConverter.Domain.Repositories
{
    public interface ICurrencyRepository
    {
        Task<List<Currency>> GetAll();
        Task<Currency> GetById(int Id);
    }
}
