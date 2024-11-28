namespace OptimaTech.BuildingManager.User.Core.Entities;

public class ApprovalRequest : EntityBase
{
   
    public required Guid RequestTypeId { get; set; }
    public required Guid ApprovalLevelId { get; set; }
    public required Status Status { get; set; }
    public DateTime? Date { get; set; }

    public RequestType? RequestType { get; set; }
    public ApprovalLevel? ApprovalLevel { get; set; }
}