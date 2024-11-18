using Gymly.Shared.Extensions;
using Gymly.Shared.Results;

public record ResultData(string Code, string Message);

public static class ResultDataExtension
{
    public static Result<T> GetFailureResult<T>(this ResultData resultData, string additionalMessage = "")
        => new(false, resultData.Message.Append(additionalMessage), resultData.Code);
}
