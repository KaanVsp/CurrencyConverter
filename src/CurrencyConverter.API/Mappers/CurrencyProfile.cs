using AutoMapper;
using CurrencyConverter.API.Models;
using CurrencyConverter.Domain.Entities;

namespace CurrencyConverter.API.Mappers
{
    public class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<Currency, CurrencyModel>()
                .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rates.FirstOrDefault().Rate));
        }
    }
}
