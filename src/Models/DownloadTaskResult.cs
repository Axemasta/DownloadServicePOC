using OneOf;

namespace DownloadServicePOC.Models;

public class DownloadTaskResult<TResult>(OneOf<Downloaded<TResult>, Failed> input) 
    : OneOfBase<Downloaded<TResult>, Failed>(input)
{
    public static DownloadTaskResult<TResult> FromSuccess(TResult result) => new(new Downloaded<TResult>(result));

    public static DownloadTaskResult<TResult> FromFailure(Exception exception) => new(new Failed(exception));
}

public record Downloaded<TResult>(TResult Result);

public record Failed(Exception Exception);