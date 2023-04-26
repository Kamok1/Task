using Abstractions;
using Implementations;
using Microsoft.Extensions.Logging;
using Models.Response;
using Moq;
namespace UnitTests;

public class MainServiceValidator
{
  [Fact]
  public void Validate_CatfactResponse_Should_Be_Invalid()
  {
    var catfact = new CatfactResponse()
    {
      Fact = "",
      Length = 2
    };
    var catfactServiceMock = new Mock<ICatfactService>();
    var loggerMock = new Mock<ILogger<MainService>>();
    catfactServiceMock.Setup(x => x.GetCatfactAsync()).ReturnsAsync(catfact);
    var storageServiceMock = new Mock<IStorageService>();

    var updateFileService = new MainService(catfactServiceMock.Object, storageServiceMock.Object, loggerMock.Object);
    Assert.True(updateFileService.IsModelValid(catfact));
  }
}
