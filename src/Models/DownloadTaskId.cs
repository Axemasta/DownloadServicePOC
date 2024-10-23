namespace DownloadServicePOC.Models;

public struct DownloadTaskId : IEquatable<DownloadTaskId>
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }
    
    public override bool Equals(object? obj)
    {
        return obj is DownloadTaskId other && Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name);
    }

    public bool Equals(DownloadTaskId other)
    {
        return Id.Equals(other.Id) && Name == other.Name;
    }
}