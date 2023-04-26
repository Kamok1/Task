using Abstractions;
using Microsoft.Extensions.Logging;
using Models;

namespace Implementations;
public class MainService : IMainService
{
  private readonly ICatfactService _catfactService;
  private readonly IStorageService _storageService;
  private readonly IValidator _validator;
  private readonly ILogger _logger;
  public MainService(ICatfactService catfactService, IStorageService storageService, ILogger<MainService> logger, IValidator validator)
  {
    _catfactService = catfactService;
    _storageService = storageService;
    _logger = logger;
    _validator = validator;
  }
  public async Task StartAsync()
  {
    _logger.LogInformation("Start");
    await DoWorkAsync();
    Stop();
  }
  public void ErrorHandler(Exception exception)
  {
    _logger.LogError($"Error occurred: {Environment.NewLine} {exception.Message}");
  }


  /// <summary>
  /// Start main functionality of the application.
  /// </summary>
  /// <returns>Task</returns>
  private async Task DoWorkAsync()
  {
    _logger.LogInformation("Fetching...");

    var model = await _catfactService.GetCatfactAsync();
    _logger.LogInformation("Fetching finished");
    if (_validator.IsModelValid(model) == false)
      throw new InvalidDataException();

    _logger.LogInformation("Appending...");
    await _storageService.AppendToStorageAsync(model);

    _logger.LogInformation("Appending finished");
  }
  /// <summary>
  /// Performs operations at the end of the application
  /// </summary>
  private void Stop()
  {
    _logger.LogInformation("End");
  }
}