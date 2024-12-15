using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.SelectParameters;

public class UserUnitSelectParameter : SelectParameterBase
{
    public Guid? UserId { get; set; }
    public Guid? UnitId { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public RelationType? RelationType { get; set; }
    public RelationStatus? RelationStatus { get; set; }
    public string? NoBAST { get; set; }
    public DateTime? DateBAST { get; set; }
}