namespace SpaceX.WebApi.Contracts.Responses;

public sealed record ApiResponse<T>
{
    public required bool IsSuccess { get; init; }

    public required int StatusCode { get; init; }

    public required string Message { get; init; }

    public T? Response { get; init; }

    public static ApiResponse<T> Success(
        T? response,
        int statusCode,
        string message)
    {
        return new ApiResponse<T>
        {
            IsSuccess = true,
            StatusCode = statusCode,
            Message = message,
            Response = response
        };
    }

    public static ApiResponse<T> Fail(
        T? response,
        int statusCode,
        string message)
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