using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using Abstractions;
using Models.Settings;

namespace Implementations;
public class FileService : IStorageService
{
  private readonly StorageSettings _appSettings;
  private readonly JsonSerializerOptions _jsonSerializerOptions;
  public FileService(StorageSettings appSettings)
  {
    _appSettings = appSettings;
    _jsonSerializerOptions = new JsonSerializerOptions()
    {
      WriteIndented = true,
      Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    };
  }

  public async Task AppendToStorageAsync<T>(T obj)
  {
    CreateFileIfDoesntExists();
    await File.AppendAllTextAsync(_appSettings.FilePath, ConvertObjectToJson(obj) + Environment.NewLine, 
      encoding:Encoding.UTF8);
  }

  /// <summary>
  /// Converts object into a JSON string.
  /// </summary>
  /// <typeparam name="T"></typeparam>
  /// <param name="obj"></param>
  /// <returns>A JSON representation of the value.</returns>
  private string ConvertObjectToJson<T>(T obj)
  {
    return JsonSerializer.Serialize(obj, _jsonSerializerOptions);
  }
  /// <summary>
  /// Checks if file exists, create if it doesn't.
  /// </summary>
  private void CreateFileIfDoesntExists()
  {
    if (File.Exists(_appSettings.FilePath))
      return;
    File.Create(_appSettings.FilePath);
  }
}
