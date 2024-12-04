using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Projects;

public class SearchProjectUseCase(IBusinessRepository<ProjectApplicationModel> repository) : IUseCase<ProjectSelectParameter, SelectResult<ProjectApplicationModel>>
{
    protected readonly IBusinessRepository<ProjectApplicationModel> _repository = repository;
    public async Task<SelectResult<ProjectApplicationModel>?> Execute(ProjectSelectParameter input)
    {
        return await _repository.SelectAsync(input);
    }
}