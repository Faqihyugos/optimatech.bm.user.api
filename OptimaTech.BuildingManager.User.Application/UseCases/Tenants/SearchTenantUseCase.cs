using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Tenants;

public class SearchTenantUseCase(IBusinessRepository<TenantApplicationModel> repository) : IUseCase<TenantSelectParameter, SelectResult<TenantApplicationModel>>
{
    protected readonly IBusinessRepository<TenantApplicationModel> _repository = repository;
    public async Task<SelectResult<TenantApplicationModel>?> Execute(TenantSelectParameter input)
    {
        return await _repository.SelectAsync(input);
    }
}