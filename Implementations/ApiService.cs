using Abstractions;
using Models;
using Models.Settings;
using System.Net.Http.Json;

namespace Implementations;
public class ApiService : ICatfactService
{
  private readonly HttpClient _httpClient;
  public ApiService(ApiSettings apiSettings, HttpClient httpClient)
  {
    _httpClient = httpClient;
    _httpClient.BaseAddress = new Uri(apiSettings.BaseUrl);
  }
  public async Task<Catfact?> GetCatfactAsync()
  {
    var res = await _httpClient.GetAsync("fact");
    res.EnsureSuccessStatusCode();
    return await res.Content.ReadFromJsonAsync<Catfact>();
  }
}
