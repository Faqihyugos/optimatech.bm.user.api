using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OptimaTech.BuildingManager.User.Api.Models;
using OptimaTech.BuildingManager.User.Application;
using OptimaTech.BuildingManager.User.Application.Models;
using OptimaTech.BuildingManager.User.Application.SelectParameters;
using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Api.Routes;

public static class ConsumerRoute
{
    public static void Map(WebApplication app)
    {
        var v1Group = app.MapGroup("/v1/consumers").WithTags("Consumers");

        v1Group.MapGet("/", SearchConsumers).WithName("SearchConsumers").WithOpenApi();

        v1Group.MapGet("/{id:Guid}", FindConsumer).WithName("FindConsumer").WithOpenApi();

        v1Group.MapPost("/", CreateConsumer).WithName("CreateConsumer").WithOpenApi();

        v1Group.MapPut("/{id:Guid}", UpdateConsumer).WithName("UpdateConsumer").WithOpenApi();

        v1Group.MapDelete("/{id:Guid}", DeleteConsumer).WithName("DeleteConsumer").WithOpenApi();
    }

    private static async Task<Results<Ok<ApiResponse<SelectResult<ConsumerResponse>>>, NotFound>> SearchConsumers(
    [FromHeader(Name = "Tenant-Code")] Guid tenantId,
    [FromHeader(Name = "User-Code")] Guid userId,
    [FromServices] IUseCase<ConsumerSelectParameter, SelectResult<ConsumerApplicationModel>> useCase,
    [FromQuery] string? code,
    [FromQuery] string? name,
    [FromQuery] string? sortBy = "code",
    [FromQuery] string? sortDirection = "asc",
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 10)
    {
        ConsumerSelectParameter selectParameter = new ConsumerSelectParameter() { TenantId = tenantId, Code = code, Name = name, SortBy = sortBy, SortDirection = sortDirection == "asc" ? SortDirection.Ascending : SortDirection.Descending, Page = page, PageSize = pageSize };
        var result = await useCase.Execute(selectParameter);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var selectResult = new SelectResult<ConsumerResponse>();
            selectResult.TotalItems = result.TotalItems;
            selectResult.TotalPages = result.TotalPages;
            selectResult.CurrentPage = result.CurrentPage;
            selectResult.PageSize = result.PageSize;
            if (result.Data != null)
            {
                selectResult.Data = result.Data.Select(t => new ConsumerResponse() { Id = t.Id, Code = t.Code, Name = t.Name, UserId = t.UserId }).ToList();
            }

            ApiResponse<SelectResult<ConsumerResponse>> apiResponse = new ApiResponse<SelectResult<ConsumerResponse>>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = selectResult };

            return TypedResults.Ok<ApiResponse<SelectResult<ConsumerResponse>>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<ConsumerResponse>>, NotFound>> FindConsumer([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromServices] IUseCase<Guid, ConsumerApplicationModel> useCase)
    {
        ConsumerApplicationModel? result = await useCase.Execute(id);

        if (result == null)
        {
            return TypedResults.NotFound();
        }
        else
        {
            var findResult = new ConsumerResponse() { Id = result.Id, Code = result.Code, Name = result.Name, UserId = result.UserId };

            ApiResponse<ConsumerResponse> apiResponse = new ApiResponse<ConsumerResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = findResult };

            return TypedResults.Ok<ApiResponse<ConsumerResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Created<ApiResponse<ConsumerResponse>>, BadRequest<ApiResponse<ConsumerResponse>>>> CreateConsumer([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, ConsumerRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_CREATE)] IUseCase<ConsumerApplicationModel, bool> useCase)
    {
        Guid newId = Guid.NewGuid();
        ConsumerApplicationModel consumer = new ConsumerApplicationModel() { Id = newId, TenantId = tenantId, Code = request.Code, Name = request.Name, UserId = request.UserId, Deleted = false, CreatedDate = DateTime.Now, CreatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(consumer);

            ConsumerResponse consumerResponse = new ConsumerResponse() { Id = consumer.Id, Code = consumer.Code, Name = consumer.Name, UserId = consumer.UserId };
            ApiResponse<ConsumerResponse> apiResponse = new ApiResponse<ConsumerResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = consumerResponse };

            return TypedResults.Created($"/consumers/{consumer.Id}", apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<ConsumerResponse> apiResponse = new ApiResponse<ConsumerResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<ConsumerResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<ConsumerResponse>>, BadRequest<ApiResponse<ConsumerResponse>>>> UpdateConsumer([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, ConsumerRequest request, [FromKeyedServices(AppConst.SERVICE_KEY_UPDATE)] IUseCase<ConsumerApplicationModel, bool> useCase)
    {
        ConsumerApplicationModel consumer = new ConsumerApplicationModel() { Id = id, TenantId = tenantId, Code = request.Code, Name = request.Name, UserId = request.UserId, Deleted = false, UpdatedDate = DateTime.Now, UpdatedUserId = userId };

        try
        {
            bool result = await useCase.Execute(consumer);

            ConsumerResponse ConsumerResponse = new ConsumerResponse() { Id = consumer.Id, Code = consumer.Code, Name = consumer.Name, UserId = consumer.UserId };
            ApiResponse<ConsumerResponse> apiResponse = new ApiResponse<ConsumerResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>(), Data = ConsumerResponse };

            return TypedResults.Ok<ApiResponse<ConsumerResponse>>(apiResponse);
        }
        catch (Exception ex)
        {
            ApiResponse<ConsumerResponse> apiResponse = new ApiResponse<ConsumerResponse>() { IsSuccess = true, StatusCode = 200, StatusMessages = new List<string>() { Messages.Translate(ex) }, Data = null };

            return TypedResults.BadRequest<ApiResponse<ConsumerResponse>>(apiResponse);
        }
    }

    private static async Task<Results<Ok<ApiResponse<bool>>, BadRequest<ApiResponse<bool>>>> DeleteConsumer([FromHeader(Name = "Tenant-Code")] Guid tenantId, [FromHeader(Name = "User-Code")] Guid userId, Guid id, [FromKeyedServices("Consumer")] IUseCase<DeleteParameter, bool> useCase)
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