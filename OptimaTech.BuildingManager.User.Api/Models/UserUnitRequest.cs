using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Api.Models;

public record UserUnitRequest
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required Guid UserId { get; set; }
    public required Guid UnitId { get; set; }
    public required DateTime  StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required RelationType RelationType { get; set; }
    public required RelationStatus RelationStatus { get; set; }
    public required string NoBAST { get; set; }
    public required DateTime DateBAST { get; set; }
}