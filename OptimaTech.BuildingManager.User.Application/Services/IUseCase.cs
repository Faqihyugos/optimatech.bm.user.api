namespace OptimaTech.BuildingManager.User.Application.Services;
public interface IUseCase<Input, Output>
{
    Task<Output?> Execute(Input input);
}