using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Users;

public class DeleteUserUseCase(IUnitOfWork<UserApplicationModel> uow) : IUseCase<DeleteParameter, bool>
{
    protected readonly IUnitOfWork<UserApplicationModel> _uow = uow;
    public async Task<bool> Execute(DeleteParameter input)
    {
        await _uow.Repository.DeleteAsync(input.Id, input.UserId);
        await _uow.CommitAsync();

        return true;
    }
}