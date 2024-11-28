namespace OptimaTech.BuildingManager.User.Core.Entities;

public class Project : EntityBase
{
    public required string Location { get; set; }
    public required decimal Area { get; set; }
}