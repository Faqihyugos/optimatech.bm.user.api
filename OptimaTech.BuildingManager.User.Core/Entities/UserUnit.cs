namespace OptimaTech.BuildingManager.User.Core.Entities;

public class UserUnit : EntityBase
{
    public required Guid UserId { get; set; }
    public required Guid UnitId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required RelationType RelationType { get; set; }
    public required RelationStatus RelationStatus { get; set; }
    public required string NoBAST { get; set; }
    public required DateTime DateBAST { get; set; }


    public User? User{ get; set; }
    public Unit? Unit { get; set; }
}