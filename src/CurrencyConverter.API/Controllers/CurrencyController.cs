using AutoMapper;
using CurrencyConverter.API.Models;
using CurrencyConverter.Domain.Entities;
using CurrencyConverter.Domain.Repositories;
using CurrencyConverter.Domain.Services;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.API.Controllers
{
    [Route("api/Currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly ICurrencyService _currencyService;
        private readonly IMapper _mapper;

        public CurrencyController(ICurrencyRepository currencyRepository,
            ICurrencyService currencyService,
            IMapper mapper)
        {
            this._currencyRepository = currencyRepository;
            this._currencyService = currencyService;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var currencies = await _currencyRepository.GetAll();

            if (currencies.Count > 0)
            {
                var response = _mapper.Map<List<CurrencyModel>>(currencies);

                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var currency = await _currencyRepository.GetById(id);

            if (currency != null)
            {
                var response = _mapper.Map<CurrencyModel>(currency);

                return Ok(response);
            }
            else
            {
                return NoContent();
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CurrencyRateUpdateModel Model)
        {
            await _currencyRepository.UpdateRate(id, Model.Rate);

            return NoContent();
        }

        [HttpGet("{firstCurrencyId}/{secondCurrencyId}/{amount}/convert")]
        public async Task<IActionResult> Convert(int firstCurrencyId, int secondCurrencyId, double amount)
        {
            double convertedValue = await _currencyService.Convert(firstCurrencyId, secondCurrencyId, amount);

            return Ok(convertedValue);
        }
    }
}
