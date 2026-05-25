using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

using SpaceX.WebApi.Contracts.Responses;

namespace SpaceX.WebApi.Extensions;

public static class ControllerExtensions
{
    public static OkObjectResult SuccessResponse<T>(
        this ControllerBase controller,
        T response)
    {
        return controller.Ok(
            ApiResponse<T>.Success(
                response,
                StatusCodes.Status200OK,
                "success"));
    }

    public static NotFoundObjectResult NotFoundResponse(
        this ControllerBase controller,
        string response = "Not found")
    {
        return controller.NotFound(
            ApiResponse<string>.Fail(
                response,
                StatusCodes.Status404NotFound,
                "not_found"));
    }

    public static BadRequestObjectResult BadRequestResponse(
        this ControllerBase controller,
        string response = "Bad request")
    {
        return controller.BadRequest(
            ApiResponse<string>.Fail(
                response,
                StatusCodes.Status400BadRequest,
                "bad_request"));
    }

    public static BadRequestObjectResult ValidationErrorResponse(
        ModelStateDictionary modelState)
    {
        var errorMessage = modelState.Values
            .SelectMany(x => x.Errors)
            .Select(x => x.ErrorMessage)
            .FirstOrDefault() ?? "Validation error.";

        return new BadRequestObjectResult(
            ApiResponse<string>.Fail(
                errorMessage,
                StatusCodes.Status400BadRequest,
                "validation_error"));
    }
}

