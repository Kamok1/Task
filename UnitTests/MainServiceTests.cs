using Abstractions;
using Implementations;
using Microsoft.Extensions.Logging;
using Models;
using Moq;

namespace UnitTests;

public class MainServiceTests
{
  private readonly ILogger<MainService> _logger;
  private readonly IValidator _validator;
  private readonly IStorageService _storageService;
  private readonly ICatfactService _catfactService;

  public MainServiceTests()
  {
    _logger = Mock.Of<ILogger<MainService>>();
    _validator = Mock.Of<IValidator>();
    _storageService = Mock.Of<IStorageService>();
    _catfactService = Mock.Of<ICatfactService>();
  }

  [Fact]
  public async Task DoWorkAsync_Should_Append_Catfact_To_Storage()
  {
    var mainService = new MainService(_catfactService, _storageService, _logger, _validator);
    var model = new Catfact { Fact = "Test", Length = 6 };
    var mockStorageService = Mock.Get(_storageService);
    var mockCatfactService = Mock.Get(_catfactService);
    var mockValidator = Mock.Get(_validator);

    mockCatfactService.Setup(c => c.GetCatfactAsync()).ReturnsAsync(model);
    mockStorageService.Setup(s => s.AppendToStorageAsync(model)).Returns(Task.CompletedTask);
    mockValidator.Setup(v => v.IsCatfactModelValid(model)).Returns(true);

    await mainService.StartAsync();
    
    mockCatfactService.Verify(c => c.GetCatfactAsync(), Times.Once);
    mockStorageService.Verify(s => s.AppendToStorageAsync(model), Times.Once);
    mockValidator.Verify(v => v.IsCatfactModelValid(model), Times.Once);
  }
  [Fact]
  public async Task DoWorkAsync_Should_Not_Append_Catfact_To_Storage()
  {
    var mainService = new MainService(_catfactService, _storageService, _logger, _validator);
    var model = new Catfact { Fact = "Test", Length = 0 };
    var mockStorageService = Mock.Get(_storageService);
    var mockCatfactService = Mock.Get(_catfactService);
    var mockValidator = Mock.Get(_validator);

    mockCatfactService.Setup(c => c.GetCatfactAsync()).ReturnsAsync(model);
    mockStorageService.Setup(s => s.AppendToStorageAsync(model)).Returns(Task.CompletedTask);
    mockValidator.Setup(v => v.IsCatfactModelValid(model)).Returns(false);

    await Assert.ThrowsAsync<InvalidDataException>(async() => await mainService.StartAsync());
    mockStorageService.Verify(s=>s.AppendToStorageAsync(model), Times.Never);
  }
}