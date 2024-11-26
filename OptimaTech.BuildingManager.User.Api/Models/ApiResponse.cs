namespace OptimaTech.BuildingManager.User.Api.Models;

public record ApiResponse<T>
{
    public required bool IsSuccess { get; set; }
    public required Int32 StatusCode { get; set; }
    public required List<String> StatusMessages { get; set; }
    public T? Data { get; set; }
}