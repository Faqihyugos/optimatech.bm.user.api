using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Units;

 public class FindUnitUseCase(IBusinessRepository<UnitApplicationModel> repository) : IUseCase<Guid, UnitApplicationModel>
    {
        protected readonly IBusinessRepository<UnitApplicationModel> _repository = repository;
        public async Task<UnitApplicationModel?> Execute(Guid input)
        {
            return await _repository.FindAsync(input);
        }
    }