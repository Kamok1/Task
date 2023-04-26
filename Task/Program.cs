using System.Diagnostics.Metrics;
using Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Task.Abstractions;
using Microsoft.Extensions.Http;
using Models.Response;
using Models.Settings;

var config = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();
var apiSettings = config.GetSection("ApiSettings").Get<ApiSettings>()!;

var host = Host.CreateDefaultBuilder(args)
  .ConfigureServices(services =>
  {
    services.AddSingleton(config.GetSection("AppSettings").Get<AppSettings>()!);
    services.AddScoped<ICatfactService, ApiService>();
    services.AddScoped<IStorageService, StorageService>();
    services.AddHttpClient<ICatfactService, ApiService>();
    services.AddSingleton(apiSettings);
  })
  .Build();

var test = host.Services.GetRequiredService<ICatfactService>();
var a = await test.GetCatfactAsync();


//todo logger
//todo exception handling
//todo comments