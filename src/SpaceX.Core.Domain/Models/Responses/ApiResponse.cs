namespace SpaceX.Core.Domain.Models.Responses;

public sealed class ApiResponse<T>
{
    public bool IsSuccess { get; init; }

    public int StatusCode { get; init; }

    public string Message { get; init; } = string.Empty;

    public T? Response { get; init; }

    public static ApiResponse<T> Success(T? response, int statusCode, string message)
    {
        return new ApiResponse<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message,
            Response = response
        };
    }

    public static ApiResponse<T> Fail(T? response, int statusCode, string message)
    {
        return new ApiResponse<T>
        {
            IsSuccess = false,
            StatusCode = statusCode,
            Message = message,
            Response = response
        };
    }
}

