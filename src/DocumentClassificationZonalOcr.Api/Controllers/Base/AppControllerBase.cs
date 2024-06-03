using System.Collections;
using DocumentClassificationZonalOcr.Api.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CaptureSolution.AutomaticReleaseApi.Controllers.Base;

[ApiController]
public class AppControllerBase : ControllerBase
{
    public IActionResult CustomResult<T>(Result<T> result, string fileExtension = "application/zip", string fileName = null)
    {
        if (result.IsSuccess)
        {

            if (typeof(IEnumerable<object>).IsAssignableFrom(typeof(T)))
            {
                int count = (result.Value as IEnumerable<object>)?.Count() ?? 0;
                var responseData = new { Count = count, Items = result.Value };
                return new OkObjectResult(new BaseResponse<object>(responseData, result.StatusCode));
            }

            else
            {
                return new OkObjectResult(new BaseResponse<T>(result.Value, result.StatusCode));
            }
        }
        else
        {
            return new ObjectResult(new BaseResponse<T>(result.Error, new List<string> { result.Error.Message }, result.StatusCode, succeeded: false))
            {
                StatusCode = (int)result.StatusCode
            };
        }
    }


    #region HandleFailure
    // No need for this but i doesn't remove the code of it
    protected IActionResult HandleFailure<T>(Result<T> result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            _ => BadRequest(
                CreateProblemDetails(
                    "Bad Request",
                    StatusCodes.Status400BadRequest,
                    result.Error))
        };
    }


    private static ProblemDetails CreateProblemDetails(
    string title,
    int status,
    Error error,
    Error[]? errors = null) => new()
    {
        Title = title,
        Status = status,
        Type = error.Code,
        Detail = error.Message,
        Extensions = { { nameof(errors), errors } }
    };
    #endregion

}
