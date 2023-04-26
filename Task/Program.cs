using Abstractions;
using Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Models.Settings;

var config = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();


var host = Host.CreateDefaultBuilder(args)
  .ConfigureLogging((_, logger) =>
  {
    logger.SetMinimumLevel(LogLevel.Error);
  })
  .ConfigureServices(services =>
  {
    services.AddSingleton(config.GetSection("StorageSettings").Get<StorageSettings>()!);
    services.AddSingleton(config.GetSection("ApiSettings").Get<ApiSettings>()!);
    services.AddSingleton<IValidator, Validator>();
    services.AddScoped<ICatfactService, ApiService>();
    services.AddScoped<IStorageService, FileService>();
    services.AddScoped<IMainService, MainService>();
    services.AddHttpClient<ICatfactService, ApiService>();
  })
  .Build();

var mainService = host.Services.GetRequiredService<IMainService>();

try
{
  await mainService.StartAsync();
}
catch (Exception e)
{
  mainService.ErrorHandler(e);
}
