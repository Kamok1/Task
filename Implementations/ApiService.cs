using System.Net.Http.Json;
using Models.Response;
using Models.Settings;
using Task.Abstractions;

namespace Implementations;
public class ApiService : ICatfactService
{
  private readonly HttpClient _httpClient;
  public ApiService(ApiSettings apiSettings, HttpClient httpClient)
  {
    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri(apiSettings.BaseUrl);
  }
  public async Task<CatfactResponse> GetCatfactAsync() 
  {
    return (await GetObject<CatfactResponse>("/fact"))!;
  }

  private async Task<T?> GetObject<T>(string path)
  {
    var res = await _httpClient.GetAsync(path);
    res.EnsureSuccessStatusCode();
    return await res.Content.ReadFromJsonAsync<T>();
  }
}
