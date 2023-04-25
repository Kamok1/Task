using System.Diagnostics.Metrics;
using Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Task.Abstractions;
using Task.Models.Settings;
using Microsoft.Extensions.Http;

var config = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();

var host = Host.CreateDefaultBuilder(args)
  .ConfigureServices(services =>
  {
    services.AddSingleton(config.GetSection("AppSettings").Get<AppSettings>()!);
    services.AddSingleton(config.GetSection("ApiSettings").Get<ApiSettings>()!);
    services.AddScoped<IDataService, ApiService>();
    services.AddScoped<IStorageService, StorageService>();
    services.AddHttpClient();
  })
  .Build();
var test = host.Services.GetRequiredService<IDataService>();
test.Test();