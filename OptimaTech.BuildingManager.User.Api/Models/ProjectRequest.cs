namespace OptimaTech.BuildingManager.User.Api.Models;

public record ProjectRequest
{
    public required string Code { get; set; }
    public required string Name { get; set; }
}