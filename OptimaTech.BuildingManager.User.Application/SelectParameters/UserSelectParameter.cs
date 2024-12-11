using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.SelectParameters;

public class UserSelectParameter : SelectParameterBase
{
     public Guid? RoleId { get; set; }
    public string? UserName { get; set; }
    public string? Password { get; set; }
    public DateTime? BirthDate { get; set; }
    public string? NIK { get; set; }
    public string? NPWP { get; set; }
    public string? Address { get; set; }
    public string? Village { get; set; }
    public string? District { get; set; }
    public string? City { get; set; }
    public string? Province { get; set; }
    public string? Email { get; set; }
    public string? MobilePhone { get; set; }
    public Religion? Religion { get; set; }
    public string? Occupation{ get; set; }
    public UserStatus? UserStatus { get; set; }
    public string? UserType { get; set; }
    public ApprovalStatus? ApprovalStatus { get; set; }
}