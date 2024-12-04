using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Projects;

public class CreateProjectUseCase(IUnitOfWork<ProjectApplicationModel> uow, IValidator<Project> validator) : IUseCase<ProjectApplicationModel, bool>
{
    protected readonly IUnitOfWork<ProjectApplicationModel> _uow = uow;
    protected readonly IValidator<Project> _validator = validator;
    public async Task<bool> Execute(ProjectApplicationModel input)
    {
        _validator.ValidateAndThrow(input.ToProject());

        await _uow.Repository.InsertAsync(input);
        await _uow.CommitAsync();

        return true;
    }
}