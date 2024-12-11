using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OptimaTech.BuildingManager.User.Api.Models;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Api.Routes;

public static class UserRoute
{
    public static void Map(WebApplication app)
    {
        var v1Group = app.MapGroup("/v1/users").WithTags("Users");

        v1Group.MapGet("/", SearchUsers).WithName("SearchUsers").WithOpenApi();

        v1Group.MapGet("/{id:Guid}", FindUser).WithName("FindUser").WithOpenApi();

        v1Group.MapPost("/", CreateUser).WithName("CreateUser").WithOpenApi();

        v1Group.MapPut("/{id:Guid}", UpdateUser).WithName("UpdateUser").WithOpenApi();

        v1Group.MapDelete("/{id:Guid}", DeleteUser).WithName("DeleteUser").WithOpenApi();
    }

    private static async Task<Results<Ok<ApiResponse<SelectResult<UserResponse>>>, NotFound>> SearchUsers(
        [FromHeader(Name = "Tenant-Code")] Guid tenantId,
        [FromHeader(Name = "User-Code")] Guid userId,
        [FromServices] IUseCase<UserSelectParameter, SelectResult<UserApplicationModel>> useCase,
        [FromQuery] string? code,
        [FromQuery] string? name,
        [FromQuery] string? username,
        [FromQuery] string? email,
        [FromQuery] string? nik,
        [FromQuery] string? sortBy = "code",
        [FromQuery] string? sortDirection = "asc",
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 10)
    {
        UserSelectParameter selectParameter = new UserSelectParameter() { TenantId = tenantId, Code = code, Name = name, UserName = username, Email = email, NIK = nik, SortBy = sortBy, SortDirection = sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending, Page = page, PageSize = pageSize };
        var result = await useCase.Execute(selectParameter);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var selectResult = new SelectResult<UserResponse>();
            selectResult.TotalItems = result.TotalItems;
            selectResult.TotalPages = result.TotalPages;
            selectResult.CurrentPage = result.CurrentPage;
            selectResult.PageSize = result.PageSize;
            if (result.Data != null)
            {
                selectResult.Data = result.Data.Select(t => new UserResponse() { Id = t.Id, RoleId = t.RoleId, Code = t.Code, Name = t.Name, UserName = t.UserName, Email = t.Email,
                BirthDate = t.BirthDate, ApprovalStatus = t.ApprovalStatus, UserStatus = t.UserStatus, UserType = t.UserType, Occupation = t.Occupation}).ToList();
            }

            ApiResponse<SelectResult<UserResponse>> apiResponse = new ApiResponse<SelectResult<UserResponse>>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = selectResult };

            return TypedResults.Ok<ApiResponse<SelectResult<UserResponse>>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<UserResponse>>, NotFound>> FindUser([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromServices] IUseCase<Guid, UserApplicationModel> useCase)
    {
        UserApplicationModel? result = await useCase.Execute(id);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var findResult = new UserResponse() { Id = result.Id, RoleId = result.RoleId, Code = result.Code, Name = result.Name,  UserName = result.UserName, Email = result.Email,
                BirthDate = result.BirthDate, ApprovalStatus = result.ApprovalStatus, UserStatus = result.UserStatus, UserType = result.UserType, Occupation = result.Occupation };

            ApiResponse<UserResponse> apiResponse = new ApiResponse<UserResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = findResult };

            return TypedResults.Ok<ApiResponse<UserResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Created<ApiResponse<UserResponse>>, BadRequest<ApiResponse<UserResponse>>>> CreateUser([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, UserRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_CREATE)] IUseCase<UserApplicationModel, bool> useCase)
    {
        Guid newId = Guid.NewGuid();
        UserApplicationModel user = new UserApplicationModel() { Id = newId, TenantId = tenantId, Code = request.Code, Name = request.Name, RoleId = request.RoleId, UserName = request.UserName,
             Email = request.Email, Password = request.Password, BirthDate = request.BirthDate, Occupation = request.Occupation, UserStatus = request.UserStatus, UserType = request.UserType, ApprovalStatus = request.ApprovalStatus, Deleted = false, CreatedDate = DateTime.Now, CreatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(user);

            UserResponse UserResponse = new UserResponse() { Id = user.Id, RoleId = user.RoleId, Code = user.Code, Name = user.Name, UserName = user.UserName, Email = user.Email,
                BirthDate = user.BirthDate, ApprovalStatus = user.ApprovalStatus, UserStatus = user.UserStatus, UserType = user.UserType, Occupation = user.Occupation};
            ApiResponse<UserResponse> apiResponse = new ApiResponse<UserResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = UserResponse };

            return TypedResults.Created($"/users/{user.Id}", apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<UserResponse> apiResponse = new ApiResponse<UserResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<UserResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<UserResponse>>, BadRequest<ApiResponse<UserResponse>>>> UpdateUser([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, UserRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_UPDATE)] IUseCase<UserApplicationModel, bool> useCase)
    {
        UserApplicationModel user = new UserApplicationModel() { Id = id, TenantId = tenantId, Code = request.Code, Name = request.Name,RoleId = request.RoleId, UserName = request.UserName,
            Email = request.Email, Password = request.Password, BirthDate = request.BirthDate, Occupation = request.Occupation, UserStatus = request.UserStatus, UserType = request.UserType, ApprovalStatus = request.ApprovalStatus, Deleted = false, UpdatedDate = DateTime.Now, UpdatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(user);

            UserResponse UserResponse = new UserResponse() { Id = user.Id, RoleId = user.RoleId, Code = user.Code, Name = user.Name, UserName = user.UserName, Email = user.Email,
                BirthDate = user.BirthDate, ApprovalStatus = user.ApprovalStatus, UserStatus = user.UserStatus, UserType = user.UserType, Occupation = user.Occupation };
            ApiResponse<UserResponse> apiResponse = new ApiResponse<UserResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = UserResponse };

            return TypedResults.Ok<ApiResponse<UserResponse>>(apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<UserResponse> apiResponse = new ApiResponse<UserResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<UserResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<bool>>, BadRequest<ApiResponse<bool>>>> DeleteUser([FromHeader(Name = "User-Code")] Guid tenantId, [FromHeader(Name = "Tenant-Code")] Guid userId, Guid id, [FromKeyedServices("User")] IUseCase<DeleteParameter, bool> useCase)
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