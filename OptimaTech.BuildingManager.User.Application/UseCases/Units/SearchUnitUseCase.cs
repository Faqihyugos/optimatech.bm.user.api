using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Units;

public class SearchUnitUseCase(IBusinessRepository<UnitApplicationModel> repository) : IUseCase<UnitSelectParameter, SelectResult<UnitApplicationModel>>
{
    protected readonly IBusinessRepository<UnitApplicationModel> _repository = repository;
    public async Task<SelectResult<UnitApplicationModel>?> Execute(UnitSelectParameter input)
    {
        return await _repository.SelectAsync(input);
    }
}