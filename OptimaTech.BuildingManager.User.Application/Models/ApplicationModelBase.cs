using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.Models;
public abstract class ApplicationModelBase : EntityBase, IApplicationModel
{
    public required bool Deleted { get; set; }
    public DateTime? CreatedDate { get; set; }
    public Guid? CreatedUserId { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public Guid? UpdatedUserId { get; set; }
    public DateTime? DeletedDate { get; set; }
    public Guid? DeletedUserId { get; set; }
}