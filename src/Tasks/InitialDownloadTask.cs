using DownloadServicePOC.Models;

namespace DownloadServicePOC.Tasks;

public class InitialDownloadTask : DownloadTask<long, long>
{
    public override DownloadTaskId Id { get; } = new()
    {
        Id = new("082e9f7a-06eb-4d83-928c-b5b3cb32267f"),
        Name = "Initial Download"
    };
    
    public override async Task<long> RunAsync(long input)
    {
        // throw new NotImplementedException("I DONT WORK");
        await Task.Delay(1000);
        Console.WriteLine($"Downloaded initial item with ID: {input}");
        return input + 7;
    }
}