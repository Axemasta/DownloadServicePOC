using OneOf;

namespace DownloadServicePOC.Models;

public class DownloadTaskResults
{
    public bool AllSucceeded { get; }
    public List<TaskResult> TaskResults { get; }

    public DownloadTaskResults(bool allSucceeded, List<TaskResult> taskResults)
    {
        AllSucceeded = allSucceeded;
        TaskResults = taskResults;
    }
}

public class TaskResult
{
    public DownloadTaskId TaskId { get; }
    public OneOf<object, Exception> Result { get; }

    public TaskResult(DownloadTaskId taskId, OneOf<object, Exception> result)
    {
        TaskId = taskId;
        Result = result;
    }

    public bool IsSuccess => Result.IsT0;  // Returns true if it's a success (object), false if it's an exception
}