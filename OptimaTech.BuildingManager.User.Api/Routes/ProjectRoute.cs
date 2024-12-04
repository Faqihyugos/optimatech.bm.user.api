using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OptimaTech.BuildingManager.User.Api.Models;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Api.Routes;

public static class ProjectRoute
{
    public static void Map(WebApplication app)
    {
        var v1Group = app.MapGroup("/v1/projects").WithTags("Projects");

        v1Group.MapGet("/", SearchProjects).WithName("SearchProjects").WithOpenApi();

        v1Group.MapGet("/{id:Guid}", FindProject).WithName("FindProject").WithOpenApi();

        v1Group.MapPost("/", CreateProject).WithName("CreateProject").WithOpenApi();

        v1Group.MapPut("/{id:Guid}", UpdateProject).WithName("UpdateProject").WithOpenApi();

        v1Group.MapDelete("/{id:Guid}", DeleteProject).WithName("DeleteProject").WithOpenApi();
    }

    private static async Task<Results<Ok<ApiResponse<SelectResult<ProjectResponse>>>, NotFound>> SearchProjects(
    [FromHeader(Name = "Tenant-Code")] Guid tenantId,
    [FromHeader(Name = "User-Code")] Guid userId,
    [FromServices] IUseCase<ProjectSelectParameter, SelectResult<ProjectApplicationModel>> useCase,
    [FromQuery] string? code,
    [FromQuery] string? name,
    [FromQuery] string? sortBy = "code",
    [FromQuery] string? sortDirection = "asc",
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        ProjectSelectParameter selectParameter = new ProjectSelectParameter() { TenantId = tenantId, Code = code, Name = name, SortBy = sortBy, SortDirection = sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending, Page = page, PageSize = pageSize };
        var result = await useCase.Execute(selectParameter);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var selectResult = new SelectResult<ProjectResponse>();
            selectResult.TotalItems = result.TotalItems;
            selectResult.TotalPages = result.TotalPages;
            selectResult.CurrentPage = result.CurrentPage;
            selectResult.PageSize = result.PageSize;
            if (result.Data != null)
            {
                selectResult.Data = result.Data.Select(t => new ProjectResponse() { Id = t.Id, Code = t.Code, Name = t.Name }).ToList();
            }

            ApiResponse<SelectResult<ProjectResponse>> apiResponse = new ApiResponse<SelectResult<ProjectResponse>>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = selectResult };

            return TypedResults.Ok<ApiResponse<SelectResult<ProjectResponse>>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<ProjectResponse>>, NotFound>> FindProject([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromServices] IUseCase<Guid, ProjectApplicationModel> useCase)
    {
        ProjectApplicationModel? result = await useCase.Execute(id);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var findResult = new ProjectResponse() { Id = result.Id, Code = result.Code, Name = result.Name };

            ApiResponse<ProjectResponse> apiResponse = new ApiResponse<ProjectResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = findResult };

            return TypedResults.Ok<ApiResponse<ProjectResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Created<ApiResponse<ProjectResponse>>, BadRequest<ApiResponse<ProjectResponse>>>> CreateProject([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, ProjectRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_CREATE)] IUseCase<ProjectApplicationModel, bool> useCase)
    {
        Guid newId = Guid.NewGuid();
        ProjectApplicationModel project = new ProjectApplicationModel() { Id = newId, TenantId = tenantId, Code = request.Code, Name = request.Name, Deleted = false, CreatedDate = DateTime.Now, CreatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(project);

            ProjectResponse projectResponse = new ProjectResponse() { Id = project.Id, Code = project.Code, Name = project.Name};
            ApiResponse<ProjectResponse> apiResponse = new ApiResponse<ProjectResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = projectResponse };

            return TypedResults.Created($"/projects/{project.Id}", apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<ProjectResponse> apiResponse = new ApiResponse<ProjectResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<ProjectResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<ProjectResponse>>, BadRequest<ApiResponse<ProjectResponse>>>> UpdateProject([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, ProjectRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_UPDATE)] IUseCase<ProjectApplicationModel, bool> useCase)
    {
        ProjectApplicationModel project = new ProjectApplicationModel() { Id = id, TenantId = tenantId, Code = request.Code, Name = request.Name, Deleted = false, UpdatedDate = DateTime.Now, UpdatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(project);

            ProjectResponse projectResponse = new ProjectResponse() { Id = project.Id, Code = project.Code, Name = project.Name };
            ApiResponse<ProjectResponse> apiResponse = new ApiResponse<ProjectResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = projectResponse };

            return TypedResults.Ok<ApiResponse<ProjectResponse>>(apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<ProjectResponse> apiResponse = new ApiResponse<ProjectResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<ProjectResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<bool>>, BadRequest<ApiResponse<bool>>>> DeleteProject([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromKeyedServices("Project")] IUseCase<DeleteParameter, bool> useCase)
    {
        try
        {
            DeleteParameter deleteParameter = new DeleteParameter() { Id = id, UserId = userId };
            bool result = await useCase.Execute(deleteParameter);

            ApiResponse<bool> apiResponse = new ApiResponse<bool>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string> { Messages.DATA_DELETED }, Data = result };

            return TypedResults.Ok<ApiResponse<bool>>(apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<bool> apiResponse = new ApiResponse<bool>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string> { Messages.Translate(ex) } };

            return TypedResults.BadRequest(apiResponse);
        }
    }
}