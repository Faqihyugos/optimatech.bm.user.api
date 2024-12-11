using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Users;

public class SearchUserUseCase(IBusinessRepository<UserApplicationModel> repository) : IUseCase<UserSelectParameter, SelectResult<UserApplicationModel>>
{
    protected readonly IBusinessRepository<UserApplicationModel> _repository = repository;
    public async Task<SelectResult<UserApplicationModel>?> Execute(UserSelectParameter input)
    {
        return await _repository.SelectAsync(input);
    }
}