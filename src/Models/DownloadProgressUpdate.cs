namespace DownloadServicePOC.Models;

public record DownloadProgressUpdate(string CurrentTaskName, int TaskTotal, int TasksCompleted);