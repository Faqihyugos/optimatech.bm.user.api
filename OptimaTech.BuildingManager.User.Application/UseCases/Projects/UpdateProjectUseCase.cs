using FluentValidation;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Application.UseCases.Projects;

public class UpdateProjectUseCase(IUnitOfWork<ProjectApplicationModel> uow, IValidator<Project> validator) : IUseCase<ProjectApplicationModel, bool>
{
    protected readonly IUnitOfWork<ProjectApplicationModel> _uow = uow;
    protected readonly IValidator<Project> _validator = validator;
    public async Task<bool> Execute(ProjectApplicationModel input)
    {
        ProjectApplicationModel? Project = await _uow.Repository.FindAsync(input.Id);
        if (Project != null)
        {
            _validator.ValidateAndThrow(input.ToProject());
            Project.Code = input.Code;
            Project.Name = input.Name;
            Project.UpdatedDate = DateTime.Now;
            Project.UpdatedUserId = input.UpdatedUserId;

            await _uow.Repository.UpdateAsync(Project);
            await _uow.CommitAsync();
        }
        else
        {
            throw new Exception(Messages.DATA_NOT_FOUND);
        }

        return true;
    }
}