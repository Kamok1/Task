namespace Models.Response;

public record CatfactResponse()
{
  public string Fact { get; set; } 
  public int Length { get; set; } 
}