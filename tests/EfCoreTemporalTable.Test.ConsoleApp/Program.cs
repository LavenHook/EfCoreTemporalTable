
using EfCoreTemporalTable.Test.SampleModel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using EntityFrameworkCore.TemporalTables.Extensions;

namespace EfCoreTemporalTable.Test.ConsoleApp
{
  class Program
  {
    static void Main(string[] args)
    {
      CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
      Host.CreateDefaultBuilder(args)
        .ConfigureLogging(builder =>
        {
          //builder.ClearProviders();
          //builder.SetMinimumLevel(LogLevel.Error);
        })
        .ConfigureAppConfiguration((Context, Builder) =>
        {
          Builder.AddJsonFile($"{typeof(Program).Namespace}.json", optional: true);
        })
        .ConfigureHostConfiguration(configHost =>
        {
          configHost.SetBasePath(Directory.GetCurrentDirectory());
          //TODO: add environment settings file
          configHost.AddEnvironmentVariables();
          configHost.AddCommandLine(args);
        })
        .ConfigureServices((hostContext, services) =>
        {
          var config = hostContext.Configuration;
          services.AddDbContext<ExampleContext>((provider, options) =>
          {
            options.UseSqlServer(hostContext.Configuration.GetConnectionString("DefaultConnection"));
            options.UseInternalServiceProvider(provider);
          });
          services.AddEntityFrameworkSqlServer();
          services.RegisterTemporalTablesForDatabase<ExampleContext>();
          services.AddHostedService<Startup>();
        });
  }
}
