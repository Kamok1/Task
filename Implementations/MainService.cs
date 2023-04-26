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
    Console.WriteLine("Start");
    await DoWorkAsync();
    Stop();
  }
  public void ErrorHandler(Exception exception)
  {
    _logger.LogError("Error occurred: {message}", exception.Message);
  }


  /// <summary>
  /// Start main functionality of the application.
  /// </summary>
  /// <returns>Task</returns>
  private async Task DoWorkAsync()
  {
    Console.WriteLine("Fetching a new fact about cats...");

    var model = await _catfactService.GetCatfactAsync();
    Console.WriteLine("Fetching finished");
    Console.WriteLine("Validating...");
    if (_validator.IsCatfactModelValid(model) == false)
      throw new InvalidDataException();

    Console.WriteLine("Model is valid");
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