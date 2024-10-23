using DownloadServicePOC.Models;

namespace DownloadServicePOC.Tasks;

public class ProcessingDownloadTask() : DownloadTask<string, string>(TaskIds.ProcessingDownloadTaskId)
{
    public override DownloadTaskId? DependantTaskId => TaskIds.RelatedDownloadTaskId;
    
    public override async Task<string> RunAsync(string input)
    {
        input = input.ToUpper();
        await Task.Delay(500);
        Console.WriteLine($"Processed download result: {input}");
        return $"Processed {input}";
    }
}