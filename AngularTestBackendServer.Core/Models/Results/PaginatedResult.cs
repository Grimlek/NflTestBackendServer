namespace AngularTestBackendServer.Core.Models.Results;

public class PaginatedResult<T>
{
    public T Results { get; set; }
    
    public int Page { get; set; }
    public int PageSize { get; set; }
    public long? TotalCount { get; set; }
}