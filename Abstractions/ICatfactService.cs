using Models.Response;

namespace Task.Abstractions;
public interface ICatfactService
{
  /// <summary>
  /// Deserializes the random object from data source into a instance of type CatfactResponse. 
  /// </summary>
  /// <returns name="CatfactResponse">Instance of CatfactResponse</returns>
  /// <exception cref="HttpRequestException"></exception>
  Task<CatfactResponse> GetCatfactAsync();
}
