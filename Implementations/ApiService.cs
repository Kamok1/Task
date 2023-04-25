using System.Net;
using Task.Abstractions;
using Task.Models.Settings;

namespace Implementations;
public class ApiService : IDataService
{
  private readonly ApiSettings _apiSettings;
  private readonly HttpClient _httpClient;
  public ApiService(ApiSettings apiSettings, IHttpClientFactory )
  {
    _apiSettings = apiSettings;
  }

  public void Test()
  {
    Console.WriteLine(_apiSettings.Source);
  }
}
