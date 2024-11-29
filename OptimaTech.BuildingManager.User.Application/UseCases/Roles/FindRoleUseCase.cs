using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Roles;

 public class FindRoleUseCase(IBusinessRepository<RoleApplicationModel> repository) : IUseCase<Guid, RoleApplicationModel>
    {
        protected readonly IBusinessRepository<RoleApplicationModel> _repository = repository;
        public async Task<RoleApplicationModel?> Execute(Guid input)
        {
            return await _repository.FindAsync(input);
        }
    }