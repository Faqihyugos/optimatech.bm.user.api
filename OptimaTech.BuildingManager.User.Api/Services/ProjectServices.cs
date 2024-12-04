using FluentValidation;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Application.UseCases.Projects;
using OptimaTech.BuildingManager.User.Core.Entities;
using OptimaTech.BuildingManager.User.Core.Validators;
using OptimaTech.BuildingManager.User.Infrastructure.Repositories;
using OptimaTech.BuildingManager.User.Infrastructure.UnitOfWork;

namespace OptimaTech.BuildingManager.User.Api.Services;

public static class ProjectServices
{
    public static void Add(WebApplicationBuilder builder)
    {
        // Add Repository
        builder.Services.AddScoped<IBusinessRepository<ProjectApplicationModel>, ProjectRepository>();

        // Add Unit Of Work
        builder.Services.AddScoped<IUnitOfWork<ProjectApplicationModel>, ProjectUnitOfWork>();

        // Add Use Cases
        builder.Services.AddScoped<IUseCase<Guid, ProjectApplicationModel>, FindProjectUseCase>();
        builder.Services.AddScoped<IUseCase<ProjectSelectParameter, SelectResult<ProjectApplicationModel>>, SearchProjectUseCase>();
        builder.Services.AddKeyedScoped<IUseCase<DeleteParameter, bool>, DeleteProjectUseCase>("Project");
        builder.Services.AddKeyedScoped<IUseCase<ProjectApplicationModel, bool>, CreateProjectUseCase>(AppConst.SERVICE_KEY_CREATE);
        builder.Services.AddKeyedScoped<IUseCase<ProjectApplicationModel, bool>, UpdateProjectUseCase>(AppConst.SERVICE_KEY_UPDATE);

        // Add Validator
        builder.Services.AddScoped<IValidator<Project>, ProjectValidator>();
    }
}