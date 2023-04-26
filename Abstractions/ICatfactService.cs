using Models;

namespace Abstractions;
public interface ICatfactService
{
  /// <summary>
  /// Deserializes the random object from data source into a instance of type CatfactResponse. 
  /// </summary>
  /// <returns>Instance of CatfactResponse</returns>
  Task<Catfact?> GetCatfactAsync();
}
