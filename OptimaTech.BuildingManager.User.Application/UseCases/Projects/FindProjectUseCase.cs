using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Projects;

 public class FindProjectUseCase(IBusinessRepository<ProjectApplicationModel> repository) : IUseCase<Guid, ProjectApplicationModel>
    {
        protected readonly IBusinessRepository<ProjectApplicationModel> _repository = repository;
        public async Task<ProjectApplicationModel?> Execute(Guid input)
        {
            return await _repository.FindAsync(input);
        }
    }