using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OptimaTech.BuildingManager.User.Api.Models;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;
using OptimaTech.BuildingManager.User.Core.Entities;

namespace OptimaTech.BuildingManager.User.Api.Routes;

public static class UnitRoute
{
    public static void Map(WebApplication app)
    {
        var v1Group = app.MapGroup("/v1/units").WithTags("Units");

        v1Group.MapGet("/", SearchUnits).WithName("SearchUnits").WithOpenApi();

        v1Group.MapGet("/{id:Guid}", FindUnit).WithName("FindUnit").WithOpenApi();

        v1Group.MapPost("/", CreateUnit).WithName("CreateUnit").WithOpenApi();

        v1Group.MapPut("/{id:Guid}", UpdateUnit).WithName("UpdateUnit").WithOpenApi();

        v1Group.MapDelete("/{id:Guid}", DeleteUnit).WithName("DeleteUnit").WithOpenApi();
    }

    private static async Task<Results<Ok<ApiResponse<SelectResult<UnitResponse>>>, NotFound>> SearchUnits(
    [FromHeader(Name = "Tenant-Code")] Guid tenantId,
    [FromHeader(Name = "User-Code")] Guid userId,
    [FromServices] IUseCase<UnitSelectParameter, SelectResult<UnitApplicationModel>> useCase,
    [FromQuery] string? code,
    [FromQuery] string? name,
    [FromQuery] Guid? ProjectId,
    [FromQuery] string? BuildingName,
    [FromQuery] string? UnitNumber,
    [FromQuery] int? FloorNumber,
    [FromQuery] string? UnitType,
    [FromQuery] UnitStatus? unitStatus,
    [FromQuery] string? sortBy = "code",
    [FromQuery] string? sortDirection = "asc",
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        UnitSelectParameter selectParameter = new UnitSelectParameter() { TenantId = tenantId, Code = code, Name = name,
        ProjectId = ProjectId, BuildingName = BuildingName, UnitNumber = UnitNumber, FloorNumber = FloorNumber, UnitType = UnitType, UnitStatus = unitStatus, SortBy = sortBy, SortDirection = sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending, Page = page, PageSize = pageSize };
        var result = await useCase.Execute(selectParameter);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var selectResult = new SelectResult<UnitResponse>();
            selectResult.TotalItems = result.TotalItems;
            selectResult.TotalPages = result.TotalPages;
            selectResult.CurrentPage = result.CurrentPage;
            selectResult.PageSize = result.PageSize;
            if (result.Data != null)
            {
                selectResult.Data = result.Data.Select(t => new UnitResponse() { Id = t.Id, Code = t.Code, Name = t.Name,
                ProjectId = t.ProjectId, BuildingName = t.BuildingName, UnitNumber = t.UnitNumber, FloorNumber = t.FloorNumber, UnitType = t.UnitType, UnitStatus = t.UnitStatus }).ToList();
            }

            ApiResponse<SelectResult<UnitResponse>> apiResponse = new ApiResponse<SelectResult<UnitResponse>>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = selectResult };

            return TypedResults.Ok<ApiResponse<SelectResult<UnitResponse>>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<UnitResponse>>, NotFound>> FindUnit([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromServices] IUseCase<Guid, UnitApplicationModel> useCase)
    {
        UnitApplicationModel? result = await useCase.Execute(id);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var findResult = new UnitResponse() { Id = result.Id, Code = result.Code, Name = result.Name, ProjectId = result.ProjectId, BuildingName = result.BuildingName, UnitNumber = result.UnitNumber, FloorNumber = result.FloorNumber, UnitType = result.UnitType, UnitStatus = result.UnitStatus };

            ApiResponse<UnitResponse> apiResponse = new ApiResponse<UnitResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = findResult };

            return TypedResults.Ok<ApiResponse<UnitResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Created<ApiResponse<UnitResponse>>, BadRequest<ApiResponse<UnitResponse>>>> CreateUnit([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, UnitRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_CREATE)] IUseCase<UnitApplicationModel, bool> useCase)
    {
        Guid newId = Guid.NewGuid();
        UnitApplicationModel unit = new UnitApplicationModel() { Id = newId, TenantId = tenantId, Code = request.Code, Name = request.Name,
            ProjectId = request.ProjectId, BuildingName = request.BuildingName, UnitNumber = request.UnitNumber, FloorNumber = request.FloorNumber, UnitType = request.UnitType, UnitStatus = request.UnitStatus, Deleted = false, CreatedDate = DateTime.Now, CreatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(unit);

            UnitResponse UnitResponse = new UnitResponse() { Id = unit.Id, Code = unit.Code, Name = unit.Name, ProjectId = unit.ProjectId, BuildingName = unit.BuildingName, UnitNumber = unit.UnitNumber, FloorNumber = unit.FloorNumber, UnitType = unit.UnitType, UnitStatus = unit.UnitStatus };
            ApiResponse<UnitResponse> apiResponse = new ApiResponse<UnitResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = UnitResponse };

            return TypedResults.Created($"/units/{unit.Id}", apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<UnitResponse> apiResponse = new ApiResponse<UnitResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<UnitResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<UnitResponse>>, BadRequest<ApiResponse<UnitResponse>>>> UpdateUnit([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, UnitRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_UPDATE)] IUseCase<UnitApplicationModel, bool> useCase)
    {
        UnitApplicationModel project = new UnitApplicationModel() { Id = id, TenantId = tenantId, Code = request.Code, Name = request.Name, 
        ProjectId = request.ProjectId, BuildingName = request.BuildingName, UnitNumber = request.UnitNumber, FloorNumber = request.FloorNumber, UnitType = request.UnitType, UnitStatus = request.UnitStatus, Deleted = false, UpdatedDate = DateTime.Now, UpdatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(project);

            UnitResponse UnitResponse = new UnitResponse() { Id = project.Id, Code = project.Code, Name = project.Name, ProjectId = project.ProjectId, BuildingName = project.BuildingName, UnitNumber = project.UnitNumber, FloorNumber = project.FloorNumber, UnitType = project.UnitType, UnitStatus = project.UnitStatus };
            ApiResponse<UnitResponse> apiResponse = new ApiResponse<UnitResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = UnitResponse };

            return TypedResults.Ok<ApiResponse<UnitResponse>>(apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<UnitResponse> apiResponse = new ApiResponse<UnitResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<UnitResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<bool>>, BadRequest<ApiResponse<bool>>>> DeleteUnit([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromKeyedServices("Unit")] IUseCase<DeleteParameter, bool> useCase)
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