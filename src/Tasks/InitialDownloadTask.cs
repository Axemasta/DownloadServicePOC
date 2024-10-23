using DownloadServicePOC.Models;

namespace DownloadServicePOC.Tasks;

public class InitialDownloadTask() : DownloadTask<long, long>(TaskIds.InitialDownloadTaskId)
{
    public override async Task<long> RunAsync(long input)
    {
        // throw new NotImplementedException("I DONT WORK");
        await Task.Delay(1000);
        Console.WriteLine($"Downloaded initial item with ID: {input}");
        return input + 7;
    }
}