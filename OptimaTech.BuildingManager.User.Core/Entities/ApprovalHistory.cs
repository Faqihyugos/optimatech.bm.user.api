namespace OptimaTech.BuildingManager.User.Core.Entities;

public class ApprovalHistory : EntityBase
{
    public required Guid ApprovalRequestId { get; set; }
    public required Guid UserId { get; set; }
    public required Guid ApprovalLevelId { get; set; }
    public required string Comment { get; set; }
    public required ApprovalStatus ApprovalStatus { get; set; }
    public required DateTime CreatedDate { get; set; }

    public ApprovalLevel? ApprovalLevel { get; set; }
    public ApprovalRequest? ApprovalRequest { get; set; }
    public User? User { get; set; }
}