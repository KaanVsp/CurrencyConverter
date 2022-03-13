using CurrencyConverter.Domain.Entities;
using CurrencyConverter.Domain.Repositories;

namespace CurrencyConverter.Domain.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;

        public CurrencyService(ICurrencyRepository currencyRepository)
        {
            this._currencyRepository = currencyRepository;
        }

        public async Task<double> Convert(int FirstCurrencyId, int SecondCurrencyId, double Amount)
        {
            var currencies = await _currencyRepository.GetCurrencies(FirstCurrencyId, SecondCurrencyId);

            if (currencies.Count < 2)
            {
                return 0;
            }

            double convertedAmount = this.Calculate(currencies.First(x => x.Id == FirstCurrencyId), currencies.First(x => x.Id == SecondCurrencyId), Amount);

            return convertedAmount;
        }

        private double Calculate(Currency FirstCurrency, Currency SecondCurrency, double Amount)
        {
            double firstCurrencyRate = FirstCurrency.Rates.First().Rate;
            double secondCurrencyRate = SecondCurrency.Rates.First().Rate;

            return Math.Round(secondCurrencyRate / firstCurrencyRate * Amount, 4);
        }
    }
}
