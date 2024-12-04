using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OptimaTech.BuildingManager.User.Api.Models;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Api.Routes;

public static class RoleRoute
{
    public static void Map(WebApplication app)
    {
        var v1Group = app.MapGroup("/v1/roles").WithTags("Roles");

        v1Group.MapGet("/", SearchRoles).WithName("SearchRoles").WithOpenApi();

        v1Group.MapGet("/{id:Guid}", FindRole).WithName("FindRole").WithOpenApi();

        v1Group.MapPost("/", CreateRole).WithName("CreateRole").WithOpenApi();

        v1Group.MapPut("/{id:Guid}", UpdateRole).WithName("UpdateRole").WithOpenApi();

        v1Group.MapDelete("/{id:Guid}", DeleteRole).WithName("DeleteRole").WithOpenApi();
    }

    private static async Task<Results<Ok<ApiResponse<SelectResult<RoleResponse>>>, NotFound>> SearchRoles(
    [FromHeader(Name = "Tenant-Code")] Guid tenantId,
    [FromHeader(Name = "User-Code")] Guid userId,
    [FromServices] IUseCase<RoleSelectParameter, SelectResult<RoleApplicationModel>> useCase,
    [FromQuery] string? code,
    [FromQuery] string? name,
    [FromQuery] string? sortBy = "code",
    [FromQuery] string? sortDirection = "asc",
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        RoleSelectParameter selectParameter = new RoleSelectParameter() { TenantId = tenantId, Code = code, Name = name, SortBy = sortBy, SortDirection = sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending, Page = page, PageSize = pageSize };
        var result = await useCase.Execute(selectParameter);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var selectResult = new SelectResult<RoleResponse>();
            selectResult.TotalItems = result.TotalItems;
            selectResult.TotalPages = result.TotalPages;
            selectResult.CurrentPage = result.CurrentPage;
            selectResult.PageSize = result.PageSize;
            if (result.Data != null)
            {
                selectResult.Data = result.Data.Select(t => new RoleResponse() { Id = t.Id, Code = t.Code, Name = t.Name }).ToList();
            }

            ApiResponse<SelectResult<RoleResponse>> apiResponse = new ApiResponse<SelectResult<RoleResponse>>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = selectResult };

            return TypedResults.Ok<ApiResponse<SelectResult<RoleResponse>>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<RoleResponse>>, NotFound>> FindRole([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromServices] IUseCase<Guid, RoleApplicationModel> useCase)
    {
        RoleApplicationModel? result = await useCase.Execute(id);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var findResult = new RoleResponse() { Id = result.Id, Code = result.Code, Name = result.Name };

            ApiResponse<RoleResponse> apiResponse = new ApiResponse<RoleResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = findResult };

            return TypedResults.Ok<ApiResponse<RoleResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Created<ApiResponse<RoleResponse>>, BadRequest<ApiResponse<RoleResponse>>>> CreateRole([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, TenantRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_CREATE)] IUseCase<RoleApplicationModel, bool> useCase)
    {
        Guid newId = Guid.NewGuid();
        RoleApplicationModel role = new RoleApplicationModel() { Id = newId, TenantId = tenantId, Code = request.Code, Name = request.Name, Deleted = false, CreatedDate = DateTime.Now, CreatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(role);

            RoleResponse roleResponse = new RoleResponse() { Id = role.Id, Code = role.Code, Name = role.Name};
            ApiResponse<RoleResponse> apiResponse = new ApiResponse<RoleResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = roleResponse };

            return TypedResults.Created($"/roles/{role.Id}", apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<RoleResponse> apiResponse = new ApiResponse<RoleResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<RoleResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<RoleResponse>>, BadRequest<ApiResponse<RoleResponse>>>> UpdateRole([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, TenantRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_UPDATE)] IUseCase<RoleApplicationModel, bool> useCase)
    {
        RoleApplicationModel role = new RoleApplicationModel() { Id = id, TenantId = tenantId, Code = request.Code, Name = request.Name, Deleted = false, UpdatedDate = DateTime.Now, UpdatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(role);

            RoleResponse roleResponse = new RoleResponse() { Id = role.Id, Code = role.Code, Name = role.Name };
            ApiResponse<RoleResponse> apiResponse = new ApiResponse<RoleResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = roleResponse };

            return TypedResults.Ok<ApiResponse<RoleResponse>>(apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<RoleResponse> apiResponse = new ApiResponse<RoleResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<RoleResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<bool>>, BadRequest<ApiResponse<bool>>>> DeleteRole([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromKeyedServices("Role")] IUseCase<DeleteParameter, bool> useCase)
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