namespace OptimaTech.BuildingManager.User.Application.Services;

public interface ISelectParameter
{
    Guid TenantId { get; set; }
    string? Code { get; set; }
    string? Name { get; set; }
    string? SortBy { get; set; }
    SortDirection? SortDirection { get; set; }
    int Page { get; set; }
    int PageSize { get; set; }
}