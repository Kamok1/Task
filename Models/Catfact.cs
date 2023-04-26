namespace Models;

public record Catfact()
{
    public string Fact { get; set; } = string.Empty;
    public int Length { get; set; }
}