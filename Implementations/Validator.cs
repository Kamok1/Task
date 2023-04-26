using Abstractions;
using Models;

namespace Implementations;

public class Validator : IValidator
{
  public bool IsCatfactModelValid(Catfact? model)
  {
    return (model == default || model.Length <= 0 || string.IsNullOrEmpty(model.Fact)) == false;
  }
}