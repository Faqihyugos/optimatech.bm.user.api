using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OptimaTech.BuildingManager.User.Api.Models;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Api.Routes;

public static class TenantRoute
{
    public static void Map(WebApplication app)
    {
        var v1Group = app.MapGroup("/v1/tenants").WithTags("Tenants");

        v1Group.MapGet("/", SearchTenants).WithName("SearchTenants").WithOpenApi();

        v1Group.MapGet("/{id:Guid}", FindTenant).WithName("FindTenant").WithOpenApi();

        v1Group.MapPost("/", CreateTenant).WithName("CreateTenant").WithOpenApi();

        v1Group.MapPut("/{id:Guid}", UpdateTenant).WithName("UpdateTenant").WithOpenApi();

        v1Group.MapDelete("/{id:Guid}", DeleteTenant).WithName("DeleteTenant").WithOpenApi();
    }

    private static async Task<Results<Ok<ApiResponse<SelectResult<TenantResponse>>>, NotFound>> SearchTenants(
    [FromHeader(Name = "Tenant-Code")] Guid tenantId,
    [FromHeader(Name = "User-Code")] Guid userId,
    [FromServices] IUseCase<TenantSelectParameter, SelectResult<TenantApplicationModel>> useCase,
    [FromQuery] string? code,
    [FromQuery] string? name,
    [FromQuery] string? description,
    [FromQuery] string? sortBy = "code",
    [FromQuery] string? sortDirection = "asc",
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        TenantSelectParameter selectParameter = new TenantSelectParameter() { TenantId = tenantId, Code = code, Name = name, Description = description, SortBy = sortBy, SortDirection = sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending, Page = page, PageSize = pageSize };
        var result = await useCase.Execute(selectParameter);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var selectResult = new SelectResult<TenantResponse>();
            selectResult.TotalItems = result.TotalItems;
            selectResult.TotalPages = result.TotalPages;
            selectResult.CurrentPage = result.CurrentPage;
            selectResult.PageSize = result.PageSize;
            if (result.Data != null)
            {
                selectResult.Data = result.Data.Select(t => new TenantResponse() { Id = t.Id, Code = t.Code, Name = t.Name, Description = t.Description }).ToList();
            }

            ApiResponse<SelectResult<TenantResponse>> apiResponse = new ApiResponse<SelectResult<TenantResponse>>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = selectResult };

            return TypedResults.Ok<ApiResponse<SelectResult<TenantResponse>>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<TenantResponse>>, NotFound>> FindTenant([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromServices] IUseCase<Guid, TenantApplicationModel> useCase)
    {
        TenantApplicationModel? result = await useCase.Execute(id);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var findResult = new TenantResponse() { Id = result.Id, Code = result.Code, Name = result.Name, Description = result.Description };

            ApiResponse<TenantResponse> apiResponse = new ApiResponse<TenantResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = findResult };

            return TypedResults.Ok<ApiResponse<TenantResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Created<ApiResponse<TenantResponse>>, BadRequest<ApiResponse<TenantResponse>>>> CreateTenant([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, TenantRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_CREATE)] IUseCase<TenantApplicationModel, bool> useCase)
    {
        Guid newId = Guid.NewGuid();
        TenantApplicationModel tenant = new TenantApplicationModel() { Id = newId, TenantId = tenantId, Code = request.Code, Name = request.Name, Description = request.Description, Deleted = false, CreatedDate = DateTime.Now, CreatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(tenant);

            TenantResponse tenantResponse = new TenantResponse() { Id = tenant.Id, Code = tenant.Code, Name = tenant.Name, Description = tenant.Description };
            ApiResponse<TenantResponse> apiResponse = new ApiResponse<TenantResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = tenantResponse };

            return TypedResults.Created($"/tenants/{tenant.Id}", apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<TenantResponse> apiResponse = new ApiResponse<TenantResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<TenantResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<TenantResponse>>, BadRequest<ApiResponse<TenantResponse>>>> UpdateTenant([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, TenantRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_UPDATE)] IUseCase<TenantApplicationModel, bool> useCase)
    {
        TenantApplicationModel tenant = new TenantApplicationModel() { Id = id, TenantId = tenantId, Code = request.Code, Name = request.Name, Description = request.Description, Deleted = false, UpdatedDate = DateTime.Now, UpdatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(tenant);

            TenantResponse tenantResponse = new TenantResponse() { Id = tenant.Id, Code = tenant.Code, Name = tenant.Name, Description = tenant.Description };
            ApiResponse<TenantResponse> apiResponse = new ApiResponse<TenantResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = tenantResponse };

            return TypedResults.Ok<ApiResponse<TenantResponse>>(apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<TenantResponse> apiResponse = new ApiResponse<TenantResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<TenantResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<bool>>, BadRequest<ApiResponse<bool>>>> DeleteTenant([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromKeyedServices("Tenant")] IUseCase<DeleteParameter, bool> useCase)
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