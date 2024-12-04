using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Api.Models;

public record UnitResponse
{
    public required Guid Id { get; set; }
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required Guid ProjectId { get; set; }
    public required string BuildingName { get; set; }
    public required string UnitNumber { get; set; }
    public required int FloorNumber { get; set; }
    public required string UnitType { get; set; }
    public required UnitStatus UnitStatus { get; set; }
}