namespace DownloadServicePOC.Models;

public abstract class DownloadTask<TInput, TOutput>(DownloadTaskId id)
{
    public DownloadTaskId Id => id;
    
    public abstract Task<TOutput> RunAsync(TInput input);

    public virtual DownloadTaskId? DependantTaskId { get; }
}