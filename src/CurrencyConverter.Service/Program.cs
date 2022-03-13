using Autofac;
using Autofac.Extensions.DependencyInjection;
using CurrencyConverter.Data.Contexts;
using CurrencyConverter.Service;
using CurrencyConverter.Service.Utilities;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder => builder.RegisterModule(new DependencyResolver()))
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.AddDbContext<PostgreSqlContext>(options => options.UseNpgsql(configuration["ConnectionStrings:PostgreSql"]));
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddHttpClient();
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();