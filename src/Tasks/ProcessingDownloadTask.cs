using DownloadServicePOC.Models;

namespace DownloadServicePOC.Tasks;

public class ProcessingDownloadTask : DownloadTask<string, string>
{
    public override DownloadTaskId Id { get; } = new DownloadTaskId()
    {
        Id = new("890ae960-00a6-4c8d-946f-a7f950c48a56"),
        Name = "Processing Download"
    };
    
    public override async Task<string> RunAsync(string input)
    {
        input = input.ToUpper();
        await Task.Delay(500);
        Console.WriteLine($"Processed download result: {input}");
        return $"Processed {input}";
    }
}