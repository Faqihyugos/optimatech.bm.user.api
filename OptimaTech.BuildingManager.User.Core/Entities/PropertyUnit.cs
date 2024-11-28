namespace OptimaTech.BuildingManager.User.Core.Entities;

public class PropertyUnit: EntityBase
{
    public required Guid ProjectId { get; set; }
    public required string Location { get; set; }
    public required string BuildingName { get; set; }
    public required string UnitNumber { get; set; }
    public required int FloorNumber { get; set; }
    public required string UnitType { get; set; }
    public StatusUnit StatusUnit { get; set; }
    public TowerType TowerType { get; set; }

    public Project? Project { get; set; }
}