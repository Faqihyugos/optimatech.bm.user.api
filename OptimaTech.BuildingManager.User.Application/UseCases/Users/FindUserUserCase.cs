using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Users;

 public class FindUserUseCase(IBusinessRepository<UserApplicationModel> repository) : IUseCase<Guid, UserApplicationModel>
    {
        protected readonly IBusinessRepository<UserApplicationModel> _repository = repository;
        public async Task<UserApplicationModel?> Execute(Guid input)
        {
            return await _repository.FindAsync(input);
        }
    }