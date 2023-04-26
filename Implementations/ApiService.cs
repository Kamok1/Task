using System.Net.Http.Json;
using Abstractions;
using Models.Response;
using Models.Settings;

namespace Implementations;
public class ApiService : ICatfactService
{
  private readonly HttpClient _httpClient;
  public ApiService(ApiSettings apiSettings, HttpClient httpClient)
  {
    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri(apiSettings.BaseUrl);
  }
  public async Task<CatfactResponse?> GetCatfactAsync() 
  {
    var res = await _httpClient.GetAsync("fact");
    res.EnsureSuccessStatusCode();
    return await res.Content.ReadFromJsonAsync<CatfactResponse>();
  }
}
