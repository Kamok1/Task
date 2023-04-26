using Models;

namespace Abstractions;

public interface IValidator
{
  /// <summary>
  /// Checks if Catfact model is valid
  /// </summary>
  /// <param name="model">Model to validate</param>
  /// <returns>True if model is valid, otherwise false</returns>
  bool IsCatfactModelValid(Catfact? model);
}