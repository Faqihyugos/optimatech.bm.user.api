using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Roles;

public class SearchRoleUseCase(IBusinessRepository<RoleApplicationModel> repository) : IUseCase<RoleSelectParameter, SelectResult<RoleApplicationModel>>
{
    protected readonly IBusinessRepository<RoleApplicationModel> _repository = repository;
    public async Task<SelectResult<RoleApplicationModel>?> Execute(RoleSelectParameter input)
    {
        return await _repository.SelectAsync(input);
    }
}