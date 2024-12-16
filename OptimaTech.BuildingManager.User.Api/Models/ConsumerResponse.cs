namespace OptimaTech.BuildingManager.User.Api.Models;

public record ConsumerResponse
{
    public required Guid Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required Guid UserId { get; set; }
}