using Models;

namespace Abstractions;

public interface IValidator
{
  bool IsModelValid(Catfact? model);
}