using Models;

namespace Abstractions;

public interface IMainService
{
  /// <summary>
  /// Asynchronous method that runs main functionality of the application and wraps it with try/catch.
  /// This method should be called at the beginning of the application.
  /// </summary>
  /// <returns>Task</returns>
  Task StartAsync();
  void ErrorHandler(Exception exception);
}