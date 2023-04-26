using Abstractions;
using Microsoft.Extensions.Logging;
using Models.Response;

namespace Implementations;
public class MainService : IMainService
{
  private readonly ICatfactService _catfactService;
  private readonly IStorageService _storageService;
  private readonly ILogger _logger;
  public MainService(ICatfactService catfactService, IStorageService storageService, ILogger<MainService> logger)
  {
    _catfactService = catfactService;
    _storageService = storageService;
    _logger = logger;
  }
  public async Task StartAsync()
  {
    _logger.LogInformation("Start");
    try
    {
      await DoWork();
    }
    catch (Exception e)
    {
      Console.WriteLine($"Error occurred: {Environment.NewLine} {e.Message}");
      throw;
    }
    Stop();
  }

  public void ErrorHandler(Exception exception)
  {
    throw new NotImplementedException();
  }

  public bool IsModelValid(CatfactResponse? model)
  {
    return (model == default || model.Length <= 0 || string.IsNullOrEmpty(model.Fact)) == false;
  }

  /// <summary>
  /// Start main functionality of the application.
  /// </summary>
  /// <returns>Task</returns>
  private async Task DoWork()
  {
    Console.WriteLine("Fetching...");

    var model = await _catfactService.GetCatfactAsync();
    model.Fact = "";
   

    Console.WriteLine("Fetching finished");
    Console.WriteLine("Appending...");

    await _storageService.AppendToStorageAsync(model);

    Console.WriteLine("Appending finished");
  }
  /// <summary>
  /// Performs operations at the end of the application
  /// </summary>
  private void Stop()
  {
    Console.WriteLine("End");
  }
}