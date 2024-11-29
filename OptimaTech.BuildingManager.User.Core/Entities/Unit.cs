namespace OptimaTech.BuildingManager.User.Core.Entities;

public class Unit: EntityBase
{
    public required Guid ProjectId { get; set; }
    public required string BuildingName { get; set; }
    public required string UnitNumber { get; set; }
    public required int FloorNumber { get; set; }
    public required string UnitType { get; set; }
    public required UnitStatus UnitStatus { get; set; }

    public Project? Project { get; set; }
}