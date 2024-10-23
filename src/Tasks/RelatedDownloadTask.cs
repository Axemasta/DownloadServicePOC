using DownloadServicePOC.Models;

namespace DownloadServicePOC.Tasks;

public class RelatedDownloadTask : DownloadTask<long, string>
{
    public override DownloadTaskId Id { get; } = new DownloadTaskId()
    {
        Id = new("7c7aea4f-66bc-49c9-8d6e-136d8c79a554"),
        Name = "Related Download Task",
    };

    public override async Task<string> RunAsync(long input)
    {
        await Task.Delay(500);
        Console.WriteLine($"Downloaded related item for ID: {input}");
        return $"Result for ID {input}";
    }
}