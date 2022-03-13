namespace CurrencyConverter.Domain.Services
{
    public interface ICurrencyService
    {
        Task<double> Convert(int FirstCurrencyId, int SecondCurrencyId, double Amount);
    }
}
