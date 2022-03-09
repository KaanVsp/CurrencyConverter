using Autofac;
using CurrencyConverter.Domain.DTOs;
using CurrencyConverter.Domain.Services;

namespace CurrencyConverter.API.Utilities
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LoadServices(builder);
        }

        private void LoadServices(ContainerBuilder builder)
        {
            builder.RegisterType<HttpClientRequester<IRequestModel, IResponseModel>>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
