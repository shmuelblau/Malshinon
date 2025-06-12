public class ApiResponse<T>
{
    public bool Success { get; set; }
    public T? Data { get; set; }
    public string Message { get; set; } = string.Empty;

    public static ApiResponse<T> Ok(T data, string? message = null) =>
        new ApiResponse<T> { Success = true, Data = data, Message = message ?? "Success" };

    public static ApiResponse<T> Fail(string message) =>
        new ApiResponse<T> { Success = false, Data = default, Message = message };
}
