using DownloadServicePOC.Models;

namespace DownloadServicePOC;

public class DownloadManager
{
    private readonly Dictionary<DownloadTaskId, Func<object, Task<object>>> _tasks = new();
    private readonly Dictionary<DownloadTaskId, DownloadTaskResult<object>> _taskResults = new();
    private readonly Dictionary<DownloadTaskId, DownloadTaskId?> _taskDependencies = new();
    private readonly Dictionary<DownloadTaskId, (Type InputType, Type OutputType)> _taskTypes = new();

    public void AddTask<TInput, TOutput>(DownloadTask<TInput, TOutput> downloadTask, DownloadTaskId? dependsOnTaskId = null)
    {
        if (dependsOnTaskId.HasValue)
        {
            if (!_taskTypes.ContainsKey(dependsOnTaskId.Value))
            {
                throw new ArgumentException($"Task with ID {dependsOnTaskId.Value} does not exist.");
            }

            var dependentTaskTypes = _taskTypes[dependsOnTaskId.Value];

            if (dependentTaskTypes.OutputType != typeof(TInput))
            {
                throw new InvalidOperationException(
                    $"Type mismatch: Task {dependsOnTaskId.Value} outputs {dependentTaskTypes.OutputType}, " +
                    $"but Task {downloadTask.Id} expects {typeof(TInput)} as input.");
            }
        }

        _tasks.Add(downloadTask.Id, async (input) =>
        {
            try
            {
                var result = await downloadTask.RunAsync((TInput)input);
                
                var wrappedResult = DownloadTaskResult<object>.FromSuccess(result);
                _taskResults[downloadTask.Id] = wrappedResult;

                return result;
            }
            catch (Exception ex)
            {
                var wrappedResult = DownloadTaskResult<object>.FromFailure(ex);
                _taskResults[downloadTask.Id] = wrappedResult;
                throw;
            }
        });

        _taskTypes[downloadTask.Id] = (typeof(TInput), typeof(TOutput));
        _taskDependencies[downloadTask.Id] = dependsOnTaskId;
    }

    public async Task<DownloadTaskResults> RunAllAsync<TFinal>(TFinal initialInput, IProgress<DownloadProgressUpdate> progress = null)
    {
        var taskQueue = _tasks.Keys.ToList();
        var results = new List<TaskResult>();

        object currentInput = initialInput;
       
        var totalTaskCount = _tasks.Count;
        var processedTaskCount = 0;

        foreach (var taskId in taskQueue)
        {
            processedTaskCount++;
            var dependencyId = _taskDependencies[taskId];
            
            if (!dependencyId.HasValue)
            {
                currentInput = initialInput;
                Console.WriteLine($"Starting task {taskId.Name} with initial input.");
            }
            else
            {
                if (!_taskResults.ContainsKey(dependencyId.Value))
                {
                    Console.WriteLine($"Task {taskId.Name} skipped because dependent task result is missing.");
                    results.Add(new TaskResult(taskId, new Exception("Missing dependency result")));
                    continue;
                }

                var dependentTaskResult = _taskResults[dependencyId.Value];

                if (dependentTaskResult.IsT1)
                {
                    Console.WriteLine($"Task {taskId.Name} skipped due to dependency failure on {dependencyId.Value.Name}.");
                    results.Add(new TaskResult(taskId, dependentTaskResult.AsT1.Exception));
                    continue;
                }

                currentInput = dependentTaskResult.AsT0.Result;
            }
            
            try
            {
                progress.Report(new DownloadProgressUpdate(taskId.Name, totalTaskCount, processedTaskCount));
                
                Console.WriteLine($"Executing task {taskId.Name}...");
                var taskResult = await _tasks[taskId](currentInput);

                results.Add(new TaskResult(taskId, taskResult));
                _taskResults[taskId] = DownloadTaskResult<object>.FromSuccess(taskResult);
                Console.WriteLine($"Task {taskId.Name} succeeded.");
            }
            catch (Exception ex)
            {
                results.Add(new TaskResult(taskId, ex));
                _taskResults[taskId] = DownloadTaskResult<object>.FromFailure(ex);
                Console.WriteLine($"Task {taskId.Name} failed with exception: {ex.Message}");
            }
        }

        bool allSuccess = results.All(r => r.IsSuccess);
        Console.WriteLine(allSuccess ? "All tasks completed successfully." : "Some tasks failed.");
        return new DownloadTaskResults(allSuccess, results);
    }
}