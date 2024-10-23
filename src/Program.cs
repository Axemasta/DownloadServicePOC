using DownloadServicePOC;
using DownloadServicePOC.Models;
using DownloadServicePOC.Tasks;

Console.WriteLine("Starting program...");

var downloadManager = new DownloadManager();

var initialDownloadTask = new InitialDownloadTask();
var relatedDownloadTask = new RelatedDownloadTask();
var processingDownloadTask = new ProcessingDownloadTask();
var digitSplittingTask = new DigitSplittingTask();

downloadManager.AddTask(initialDownloadTask);
downloadManager.AddTask(relatedDownloadTask);
downloadManager.AddTask(processingDownloadTask);
downloadManager.AddTask(digitSplittingTask);

var progress = new Progress<DownloadProgressUpdate>(update =>
{
    Console.WriteLine($"Started Task: {update.CurrentTaskName} ({update.TasksCompleted}/{update.TaskTotal})");
});

var result = await downloadManager.RunAllAsync(12345L, progress);

Console.WriteLine(result.AllSucceeded ? "All downloaded successfully" : "Not all downloads succeeded");