using Abstractions;
using Implementations;
using Microsoft.Extensions.Logging;
using Models;
using Moq;
namespace UnitTests;

public class ValidatorTests
{
  [Fact]
  public void Validate_CatfactResponse()
  {
    var invalidCatfact = new Catfact { Fact = "", Length = 2 };
    var invalidCatfact2 = new Catfact { Fact = "dsa", Length = 0 };

    var validator = new Validator();

    Assert.False(validator.IsModelValid(invalidCatfact));
    Assert.False(validator.IsModelValid(invalidCatfact2));
  }

  [Fact]
  public void Validate_CatfactResponse_Should_Be_Valid()
  {
    var catfact = new Catfact()
    {
      Fact = "Fact",
      Length = 321
    };
    var validator = new Validator();

    Assert.True(validator.IsModelValid(catfact));
  }
}
