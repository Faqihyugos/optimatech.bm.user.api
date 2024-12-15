using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.UserUnits;

public class SearchUserUnitUseCase(IBusinessRepository<UserUnitApplicationModel> repository) : IUseCase<UserUnitSelectParameter, SelectResult<UserUnitApplicationModel>>
{
    protected readonly IBusinessRepository<UserUnitApplicationModel> _repository = repository;
    public async Task<SelectResult<UserUnitApplicationModel>?> Execute(UserUnitSelectParameter input)
    {
        return await _repository.SelectAsync(input);
    }
}