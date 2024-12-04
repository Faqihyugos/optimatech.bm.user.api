using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.Models;

public class UnitApplicationModel : ApplicationModelBase
{
    public required Guid ProjectId { get; set; }
    public required string BuildingName { get; set; }
    public required string UnitNumber { get; set; }
    public required int FloorNumber { get; set; }
    public required string UnitType { get; set; }
    public required UnitStatus UnitStatus { get; set; }

    // Navigation Properties
    public ProjectApplicationModel? Project { get; set; }
}