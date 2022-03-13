using AutoMapper;
using CurrencyConverter.Domain.DTOs;
using CurrencyConverter.Domain.Entities;
using CurrencyConverter.Domain.Repositories;
using CurrencyConverter.Domain.Services;

namespace CurrencyConverter.Service
{
    public class Worker : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<Worker> _logger;
        private readonly IMapper _mapper;
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IHttpClientRequester<IRequestModel, IResponseModel> _genericRequester;

        public Worker(IConfiguration configuration,
            ILogger<Worker> logger,
            IMapper mapper,
            ICurrencyRepository currencyRepository,
            IHttpClientRequester<IRequestModel, IResponseModel> genericRequester)
        {
            this._configuration = configuration;
            this._logger = logger;
            this._mapper = mapper;
            this._currencyRepository = currencyRepository;
            this._genericRequester = genericRequester;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var response = await _genericRequester.SendHttpClientRequest<IRequestModel, FixerResponseDTO> (null, $"{_configuration.GetSection("Fixer:QueryPrefix").Value}{_configuration.GetSection("Fixer:ApiKey").Value}", HttpMethod.Get);

                var currencies = _mapper.Map<List<Currency>>(response.rates, opt => opt.Items["InsertTime"] = DateTime.Now);
                await _currencyRepository.AddNewRates(currencies, response.@base);

                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(new TimeSpan(24, 0, 0), stoppingToken);
            }
        }
    }
}