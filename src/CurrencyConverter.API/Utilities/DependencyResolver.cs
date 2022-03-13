using Autofac;
using CurrencyConverter.Data.Repositories;
using CurrencyConverter.Domain.DTOs;
using CurrencyConverter.Domain.Services;

namespace CurrencyConverter.API.Utilities
{
    public class DependencyResolver : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            LoadRepositories(builder);
            LoadServices(builder);
        }

        private void LoadRepositories(ContainerBuilder builder)
        {
            builder.RegisterType<CurrencyRepository>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }

        private void LoadServices(ContainerBuilder builder)
        {
            builder.RegisterType<CurrencyService>().AsImplementedInterfaces().InstancePerLifetimeScope();
            builder.RegisterType<HttpClientRequester<IRequestModel, IResponseModel>>().AsImplementedInterfaces().InstancePerLifetimeScope();
        }
    }
}
