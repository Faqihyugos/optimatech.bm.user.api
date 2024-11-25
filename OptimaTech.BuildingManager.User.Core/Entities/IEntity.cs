namespace OptimaTech.BuildingManager.User.Core.Entities;
public interface IEntity : IBasic
{
    string Code { get; set; }
    string Name { get; set; }
}