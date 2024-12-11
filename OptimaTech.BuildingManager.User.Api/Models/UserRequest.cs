using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Api.Models;

public record UserRequest
{
    public required string Code { get; set; }
    public required string Name { get; set; }
    public required Guid RoleId { get; set; }
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
    public required string Email { get; set; }
    public string? MobilePhone { get; set; }
    public Religion? Religion { get; set; }
    public required string Occupation{ get; set; }
    public required UserStatus UserStatus { get; set; }
    public required string UserType { get; set; }
    public required ApprovalStatus ApprovalStatus { get; set; }
}