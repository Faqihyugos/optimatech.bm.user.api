using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Tenants;

 public class FindTenantUseCase(IBusinessRepository<TenantApplicationModel> repository) : IUseCase<Guid, TenantApplicationModel>
    {
        protected readonly IBusinessRepository<TenantApplicationModel> _repository = repository;
        public async Task<TenantApplicationModel?> Execute(Guid input)
        {
            return await _repository.FindAsync(input);
        }
    }