namespace OptimaTech.BuildingManager.User.Api.Models;

public record ProjectResponse
{
    public required Guid Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
}