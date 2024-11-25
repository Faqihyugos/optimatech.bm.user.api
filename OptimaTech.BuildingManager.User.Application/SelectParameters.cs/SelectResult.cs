namespace OptimaTech.BuildingManager.User.Application.SelectParameters;

public class SelectResult<T> where T : class
{
    public Int32 TotalItems { get; set; }
    public int TotalPages { get; set; }
    public int CurrentPage { get; set; }
    public int PageSize { get; set; }
    public List<T>? Data { get; set; }
}