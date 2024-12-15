using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.Models;

public class UserUnitApplicationModel : ApplicationModelBase
{
    public required Guid UserId { get; set; }
    public required Guid UnitId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required RelationType RelationType { get; set; }
    public required RelationStatus RelationStatus { get; set; }
    public required string NoBAST { get; set; }
    public required DateTime DateBAST { get; set; }


    public UserApplicationModel? User{ get; set; }
    public UnitApplicationModel? Unit { get; set; }
}