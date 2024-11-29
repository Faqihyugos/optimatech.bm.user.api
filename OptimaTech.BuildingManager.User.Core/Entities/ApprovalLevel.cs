namespace OptimaTech.BuildingManager.User.Core.Entities;

public class ApprovalLevel : IBasic
{
    public required Guid Id { get; set; }
    public required Guid TenantId { get; set; }
    public required Guid RoleId { get; set; }
    public required Guid RequestTypeId { get; set; }
    public required string Name { get; set; }
    public required int sequence { get; set; }

    public Role? Role { get; set; }
    public RequestType? RequestType { get; set; }
}