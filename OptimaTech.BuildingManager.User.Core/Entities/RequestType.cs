namespace OptimaTech.BuildingManager.User.Core.Entities;

public class RequestType : IBasic
{
    public required Guid Id { get; set; }
    public required Guid TenantId { get; set; }

    public required RequestTypeName Name { get; set; }
}