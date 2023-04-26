using Implementations;
using Models.Response;
using Models.Settings;

namespace UnitTests
{
  public class ApiServiceValidator
  {
    private readonly ApiSettings _mockApiSettings = new() { BaseUrl = "https://catfact.ninja" };
    private readonly HttpClient _mockHttpClient = new();

    [Fact]
    public async System.Threading.Tasks.Task Get_Object_Should_Be_Valid()
    {
      var api = new ApiService(_mockApiSettings, _mockHttpClient);
      await api.GetDataAsync<CatfactResponse>("/fact");
    }

    [Fact]
    public async System.Threading.Tasks.Task Get_Object_Should_Be_Invalid()
    {
      try
      {
        var api = new ApiService(_mockApiSettings, _mockHttpClient);
        await api.GetDataAsync<CatfactResponse>("/fact");
      }
      catch (Exception e)
      {
        Assert.NotNull(e);
      }
    }
  }
}