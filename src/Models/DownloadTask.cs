namespace DownloadServicePOC.Models;

public abstract class DownloadTask<TInput, TOutput>
{
    public abstract DownloadTaskId Id { get; }

    public abstract Task<TOutput> RunAsync(TInput input);
}