using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Roles;

public class DeleteRoleUseCase(IUnitOfWork<RoleApplicationModel> uow) : IUseCase<DeleteParameter, bool>
{
    protected readonly IUnitOfWork<RoleApplicationModel> _uow = uow;
    public async Task<bool> Execute(DeleteParameter input)
    {
        await _uow.Repository.DeleteAsync(input.Id, input.UserId);
        await _uow.CommitAsync();

        return true;
    }
}