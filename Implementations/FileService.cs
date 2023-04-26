using Abstractions;
using Models.Settings;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

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
    await File.AppendAllTextAsync(_appSettings.FilePath, JsonSerializer.Serialize(obj, _jsonSerializerOptions) + Environment.NewLine,
      encoding: Encoding.UTF8);
  }
}
