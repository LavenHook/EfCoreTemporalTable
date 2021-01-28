using EfCoreTemporalTable.Test.SampleModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;

namespace EfCoreTemporalTable.Test.ConsoleApp
{
  public class Startup : IHostedService
  {
    private ILogger<Startup> Logger { get; }
    private readonly IHostApplicationLifetime AppLifetime;
    private CancellationTokenSource CancellationTokenSource { get; } = new CancellationTokenSource();
    private TaskCompletionSource<bool> TaskCompletionSource { get; } = new TaskCompletionSource<bool>();
    private IServiceProvider Services { get; set; }
    private ExampleContext Context { get; set; }

    public Startup(ILogger<Startup> logger, IHostApplicationLifetime appLifetime, IServiceProvider services)
    {
      Logger = logger;
      AppLifetime = appLifetime;
      Services = services;
      Context = services.GetRequiredService<ExampleContext>();
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {

      AppLifetime.ApplicationStarted.Register(OnStarted);
      AppLifetime.ApplicationStopping.Register(OnStopping);
      AppLifetime.ApplicationStopped.Register(OnStopped);


      //Task.Run(() => Run(CancellationTokenSource.Token));
      await Run(CancellationTokenSource.Token);
      TaskCompletionSource.SetResult(true);
      AppLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
      CancellationTokenSource.Cancel();
      // Defer completion promise, until our application has reported it is done.
      return Task.CompletedTask;
    }

    public async Task Run(CancellationToken cancellationToken)
    {
      Context.Database.Migrate();
      //PUT TESTS HERE
      Test1();
      await Task.CompletedTask;
    }

    private void OnStarted()
    {
      Logger.LogInformation("OnStarted has been called.");

      // Perform post-startup activities here
    }

    private void OnStopping()
    {
      Logger.LogInformation("OnStopping has been called.");

      // Perform on-stopping activities here
    }

    private void OnStopped()
    {
      Logger.LogInformation("OnStopped has been called.");

      // Perform post-stopped activities here
    }

    private void Test1()
    {
      var test1 = new ExampleEntity1() { FirstProperty = "Some example text", SecondProperty = "More example text", ThirdProperty = DateTime.Now };
      Context.ExampleEntity1.Add(test1);
      Context.SaveChanges();
      System.Threading.Thread.Sleep(10);
      test1.SecondProperty = "Changed Text";
      test1.ThirdProperty = DateTime.Now;
      Context.SaveChanges();

      var entities = Context.ExampleEntity1.AsTemporalAll();
      foreach(var e in entities)
      {
        Console.WriteLine(JsonSerializer.Serialize(e));
      }
    }
  }
}
