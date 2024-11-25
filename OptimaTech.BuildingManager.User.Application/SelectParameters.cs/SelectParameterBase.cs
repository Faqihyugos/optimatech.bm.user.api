using OptimaTech.BuildingManager.User.Application.Services;

namespace OptimaTech.BuildingManager.User.Application.SelectParameters;

public abstract class SelectParameterBase : ISelectParameter
{
    public required Guid TenantId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }
    public string? SortBy { get; set; }
    public SortDirection? SortDirection { get; set; }
    public int Page { get; set; }
    public int PageSize { get; set; }
}