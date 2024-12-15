using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.UserUnits;

 public class FindUserUnitUseCase(IBusinessRepository<UserUnitApplicationModel> repository) : IUseCase<Guid, UserUnitApplicationModel>
    {
        protected readonly IBusinessRepository<UserUnitApplicationModel> _repository = repository;
        public async Task<UserUnitApplicationModel?> Execute(Guid input)
        {
            return await _repository.FindAsync(input);
        }
    }