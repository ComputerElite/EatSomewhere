namespace EatSomewhere.Server;

public class ApiResponse
{
    public string? CreatedId { get; set; }
    public string? Error { get; set; }
    public bool Success { get; set; } = false;
}

public class ApiResponse<T> : ApiResponse
{
    public T? Data { get; set; }
}