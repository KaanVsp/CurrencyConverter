using AutoMapper;
using CurrencyConverter.Domain.Entities;

namespace CurrencyConverter.Service.Mappers
{
    class CurrencyProfile : Profile
    {
        public CurrencyProfile()
        {
            CreateMap<KeyValuePair<string, double>, Currency>()
                    .ForMember(dest => dest.Symbol, opt => opt.MapFrom(src => src.Key))
                    .ForMember(dest => dest.InsertTime, opt => opt.MapFrom((src, dest, destMember, context) => context.Items["InsertTime"]))
                    .ForMember(dest => dest.Rates, opt => opt.MapFrom((src, dest, destMember, context) => CreateCurrencyRate(context.Items["InsertTime"], src.Value)));
        }

        private List<CurrencyRate> CreateCurrencyRate(object InsertTime, double Rate)
        {
            return new List<CurrencyRate>()
            {
                 new CurrencyRate((DateTime)InsertTime, Rate)
            };
        }
    }
}
