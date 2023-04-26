namespace Abstractions;
public interface IStorageService
{
  /// <summary>
  /// Convert object to JSON and append result to storage. 
  /// </summary>
  /// <returns>Task</returns>
  Task AppendToStorageAsync<T>(T obj);
}
