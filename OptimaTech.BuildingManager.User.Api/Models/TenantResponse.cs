namespace OptimaTech.BuildingManager.User.Api.Models;

public record TenantResponse
{
    public required Guid Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
}