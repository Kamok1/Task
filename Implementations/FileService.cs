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
    await File.AppendAllTextAsync(_appSettings.FilePath, JsonSerializer.Serialize(obj, _jsonSerializerOptions) + Environment.NewLine, 
      encoding:Encoding.UTF8);
  }

  /// <summary>
  /// Checks if the file exists, creates it if it doesn't.
  /// </summary>
  private void CreateFileIfDoesntExists()
  {
    if (File.Exists(_appSettings.FilePath))
      return;
    File.Create(_appSettings.FilePath);
  }
}
