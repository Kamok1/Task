using Implementations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Models.Settings;
using Abstractions;

var config = new ConfigurationBuilder()
  .AddJsonFile("appsettings.json")
  .Build();

var host = Host.CreateDefaultBuilder(args)
  .ConfigureServices(services =>
  {
    services.AddSingleton(config.GetSection("StorageSettings").Get<StorageSettings>()!);
    services.AddSingleton(config.GetSection("ApiSettings").Get<ApiSettings>()!);
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
