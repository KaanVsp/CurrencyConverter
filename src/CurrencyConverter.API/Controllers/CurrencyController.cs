using AutoMapper;
using CurrencyConverter.API.Models;
using CurrencyConverter.Domain.Entities;
using CurrencyConverter.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.API.Controllers
{
    [Route("api/Currency")]
    [ApiController]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IMapper _mapper;

        public CurrencyController(ICurrencyRepository currencyRepository,
            IMapper mapper)
        {
            this._currencyRepository = currencyRepository;
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
    }
}
