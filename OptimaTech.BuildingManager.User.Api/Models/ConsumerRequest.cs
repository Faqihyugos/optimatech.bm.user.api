namespace OptimaTech.BuildingManager.User.Api.Models;

public record ConsumerRequest
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required Guid UserId { get; set; }
}