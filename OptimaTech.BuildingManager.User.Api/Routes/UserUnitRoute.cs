using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OptimaTech.BuildingManager.User.Api.Models;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Api.Routes;

public static class UserUnitRoute
{
    public static void Map(WebApplication app)
    {
        var v1Group = app.MapGroup("/v1/userunits").WithTags("UserUnits");

        v1Group.MapGet("/", SearchUserUnits).WithName("SearchUserUnits").WithOpenApi();

        v1Group.MapGet("/{id:Guid}", FindUserUnit).WithName("FindUserUnit").WithOpenApi();

        v1Group.MapPost("/", CreateUserUnit).WithName("CreateUserUnit").WithOpenApi();

        v1Group.MapPut("/{id:Guid}", UpdateUserUnit).WithName("UpdateUserUnit").WithOpenApi();

        v1Group.MapDelete("/{id:Guid}", DeleteUserUnit).WithName("DeleteUserUnit").WithOpenApi();
    }

    private static async Task<Results<Ok<ApiResponse<SelectResult<UserUnitResponse>>>, NotFound>> SearchUserUnits(
        [FromHeader(Name = "Tenant-Code")] Guid tenantId,
        [FromHeader(Name = "User-Code")] Guid userId,
        [FromServices] IUseCase<UserUnitSelectParameter, SelectResult<UserUnitApplicationModel>> useCase,
        [FromQuery] string? code,
        [FromQuery] string? name,
        [FromQuery] string? nobast,
        [FromQuery] string? sortBy = "code",
        [FromQuery] string? sortDirection = "asc",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        UserUnitSelectParameter selectParameter = new UserUnitSelectParameter() { TenantId = tenantId, Code = code, Name = name, NoBAST = nobast, SortBy = sortBy, SortDirection = sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending, Page = page, PageSize = pageSize };
        var result = await useCase.Execute(selectParameter);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var selectResult = new SelectResult<UserUnitResponse>();
            selectResult.TotalItems = result.TotalItems;
            selectResult.TotalPages = result.TotalPages;
            selectResult.CurrentPage = result.CurrentPage;
            selectResult.PageSize = result.PageSize;
            if (result.Data != null)
            {
                selectResult.Data = result.Data.Select(t => new UserUnitResponse() { Id = t.Id, Code = t.Code, Name = t.Name, UserId = t.UserId, UnitId = t.UnitId, NoBAST = t.NoBAST, DateBAST = t.DateBAST, EndDate = t.EndDate, StartDate = t.StartDate, RelationType = t.RelationType, RelationStatus = t.RelationStatus }).ToList();
            }

            ApiResponse<SelectResult<UserUnitResponse>> apiResponse = new ApiResponse<SelectResult<UserUnitResponse>>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = selectResult };

            return TypedResults.Ok<ApiResponse<SelectResult<UserUnitResponse>>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<UserUnitResponse>>, NotFound>> FindUserUnit([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromServices] IUseCase<Guid, UserUnitApplicationModel> useCase)
    {
        UserUnitApplicationModel? result = await useCase.Execute(id);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var findResult = new UserUnitResponse()
            {
                Id = result.Id,
                Code = result.Code,
                Name = result.Name,
                UserId = result.UserId,
                UnitId = result.UnitId,
                NoBAST = result.NoBAST,
                DateBAST = result.DateBAST,
                EndDate = result.EndDate,
                StartDate = result.StartDate,
                RelationType = result.RelationType,
                RelationStatus = result.RelationStatus,
            };

            ApiResponse<UserUnitResponse> apiResponse = new ApiResponse<UserUnitResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = findResult };

            return TypedResults.Ok<ApiResponse<UserUnitResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Created<ApiResponse<UserUnitResponse>>, BadRequest<ApiResponse<UserUnitResponse>>>> CreateUserUnit([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, UserUnitRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_CREATE)] IUseCase<UserUnitApplicationModel, bool> useCase)
    {
        Guid newId = Guid.NewGuid();
        UserUnitApplicationModel userUnit = new UserUnitApplicationModel()
        {
            Id = newId,
            TenantId = tenantId,
            Code = request.Code,
            Name = request.Name,
            UserId = request.UserId,
            UnitId = request.UnitId,
            NoBAST = request.NoBAST,
            DateBAST = request.DateBAST,
            EndDate = request.EndDate,
            StartDate = request.StartDate,
            RelationType = request.RelationType,
            RelationStatus = request.RelationStatus,

            Deleted = false,
            CreatedDate = DateTime.Now,
            CreatedUserId = userId
        };

        try
        {
            bool result = await useCase.Execute(userUnit);

            UserUnitResponse userUnitResponse = new UserUnitResponse()
            {
                Id = userUnit.Id,
                UserId = userUnit.UserId,
                UnitId = userUnit.UnitId,
                Code = userUnit.Code,
                Name = userUnit.Name,
                NoBAST = userUnit.NoBAST,
                DateBAST = userUnit.DateBAST,
                EndDate = userUnit.EndDate,
                StartDate = userUnit.StartDate,
                RelationType = userUnit.RelationType,
                RelationStatus = userUnit.RelationStatus,
            };
            ApiResponse<UserUnitResponse> apiResponse = new ApiResponse<UserUnitResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = userUnitResponse };

            return TypedResults.Created($"/userunits/{userUnit.Id}", apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<UserUnitResponse> apiResponse = new ApiResponse<UserUnitResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<UserUnitResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<UserUnitResponse>>, BadRequest<ApiResponse<UserUnitResponse>>>> UpdateUserUnit([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, UserUnitRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_UPDATE)] IUseCase<UserUnitApplicationModel, bool> useCase)
    {
        UserUnitApplicationModel user = new UserUnitApplicationModel()
        {
            Id = id,
            TenantId = tenantId,
            Code = request.Code,
            Name = request.Name,
            UserId = request.UserId,
            UnitId = request.UnitId,
            NoBAST = request.NoBAST,
            DateBAST = request.DateBAST,
            EndDate = request.EndDate,
            StartDate = request.StartDate,
            RelationType = request.RelationType,
            RelationStatus = request.RelationStatus,
            Deleted = false,
            UpdatedDate = DateTime.Now,
            UpdatedUserId = userId
        };

        try
        {
            bool result = await useCase.Execute(user);

            UserUnitResponse UserUnitResponse = new UserUnitResponse()
            {
                Id = user.Id,
                Code = user.Code,
                Name = user.Name,
                UnitId = user.UnitId,
                UserId = user.UserId,
                NoBAST = user.NoBAST,
                DateBAST = user.DateBAST,
                EndDate = user.EndDate,
                StartDate = user.StartDate,
                RelationType = user.RelationType,
                RelationStatus = user.RelationStatus
            };
            ApiResponse<UserUnitResponse> apiResponse = new ApiResponse<UserUnitResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = UserUnitResponse };

            return TypedResults.Ok<ApiResponse<UserUnitResponse>>(apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<UserUnitResponse> apiResponse = new ApiResponse<UserUnitResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<UserUnitResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<bool>>, BadRequest<ApiResponse<bool>>>> DeleteUserUnit([FromHeader(Name = "User-Code")] Guid tenantId, [FromHeader(Name = "Tenant-Code")] Guid userId, Guid id, [FromKeyedServices("UserUnit")] IUseCase<DeleteParameter, bool> useCase)
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