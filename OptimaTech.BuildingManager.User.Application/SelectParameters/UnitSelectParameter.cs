using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.SelectParameters;

public class UnitSelectParameter : SelectParameterBase
{
    public  required Guid? ProjectId { get; set; }
    public  string? BuildingName { get; set; }
    public  string? UnitNumber { get; set; }
    public  int? FloorNumber { get; set; }
    public  string? UnitType { get; set; }
    public  UnitStatus? UnitStatus { get; set; }
}