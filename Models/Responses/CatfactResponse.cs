namespace Models.Response;

public record CatfactResponse()
{
  public string Fact { get; set; } = string.Empty;
  public int Length { get; set; } 
}