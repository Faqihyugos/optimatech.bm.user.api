namespace OptimaTech.BuildingManager.User.Core.Entities;

public class User : EntityBase
{
    public required Guid RoleId { get; set; }
    public required Guid PropertyUnitId { get; set; }
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public required DateTime BirthDate { get; set; }
    public string? NIK { get; set; }
    public string? NPWP { get; set; }
    public string? Address { get; set; }
    public string? Village { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? Email { get; set; }
    public string? MobilePhone { get; set; }
    public Religion Religion { get; set; }
    public Occupation occupation{ get; set; }
    public required Status Status { get; set; }
    public required UserType UserType { get; set; }
    public required string UnitNumber { get; set; }
    public required string NoBAST{ get; set; }
    public required DateTime DateBAST{ get; set; }
    public required Status StatusApproval { get; set; }
    

    public Role? Role { get; set; }
    public PropertyUnit? PropertyUnit { get; set; }

}