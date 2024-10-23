using DownloadServicePOC.Models;

namespace DownloadServicePOC.Tasks;

public class DigitSplittingTask() : DownloadTask<long, long[]>(TaskIds.DigitSplittingTaskId)
{
    public override async Task<long[]> RunAsync(long input)
    {
        var inputString = input.ToString();
        var digits = inputString.Select(s => long.Parse(s.ToString())).ToArray();
    
        await Task.Delay(500);
        Console.WriteLine($"Turned input: {input} into digits: {string.Join(", ", digits)}");
        return digits;
    }
}