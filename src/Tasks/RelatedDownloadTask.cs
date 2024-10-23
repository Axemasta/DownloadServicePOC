using DownloadServicePOC.Models;

namespace DownloadServicePOC.Tasks;

public class RelatedDownloadTask() : DownloadTask<long, string>(TaskIds.RelatedDownloadTaskId)
{
    public override DownloadTaskId? DependantTaskId => TaskIds.InitialDownloadTaskId;

    public override async Task<string> RunAsync(long input)
    {
        await Task.Delay(500);
        Console.WriteLine($"Downloaded related item for ID: {input}");
        return $"Result for ID {input}";
    }
}