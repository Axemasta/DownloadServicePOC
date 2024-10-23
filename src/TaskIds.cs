using DownloadServicePOC.Models;

namespace DownloadServicePOC;

public static class TaskIds
{
    public static readonly DownloadTaskId InitialDownloadTaskId = new()
    {
        Id = new("082e9f7a-06eb-4d83-928c-b5b3cb32267f"),
        Name = "Initial Download"
    };
    
    public static readonly DownloadTaskId ProcessingDownloadTaskId = new()
    {
        Id = new("890ae960-00a6-4c8d-946f-a7f950c48a56"),
        Name = "Processing Download"
    };
    
    public static readonly DownloadTaskId RelatedDownloadTaskId = new()
    {
        Id = new("7c7aea4f-66bc-49c9-8d6e-136d8c79a554"),
        Name = "Related Download Task",
    };
    
    public static readonly DownloadTaskId DigitSplittingTaskId = new()
    {
        Id = new("e3ebff5a-ae37-45df-aab2-71fe06dba4d5"),
        Name = "Split Digits"
    };
}