namespace OptimaTech.BuildingManager.User.Core.Entities;

public class UserProperty : IBasic
{
    public required Guid Id { get; set; }
    public required Guid TenantId { get; set; }
    public required Guid UserId { get; set; }
    public required Guid PropertyUnitId { get; set; }
    public required DateTime StartDate { get; set; }
    public required DateTime EndDate { get; set; }
    public required string Type { get; set; }
    public required string Status { get; set; }
    public required string NoBAST { get; set; }
    public required DateTime DateBAST { get; set; }


    public User? User{ get; set; }
    public PropertyUnit? PropertyUnit { get; set; }
}