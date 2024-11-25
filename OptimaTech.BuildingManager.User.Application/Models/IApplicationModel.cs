using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.Models;

public interface IApplicationModel : IEntity
{
    bool Deleted { get; set; }
    DateTime? CreatedDate { get; set; }
    Guid? CreatedUserId { get; set; }
    DateTime? UpdatedDate { get; set; }
    Guid? UpdatedUserId { get; set; }
    DateTime? DeletedDate { get; set; }
    Guid? DeletedUserId { get; set; }
}