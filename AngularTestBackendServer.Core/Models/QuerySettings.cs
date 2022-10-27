using AngularTestBackendServer.Core.Models.Filters;
using AngularTestBackendServer.Core.Specifications;

namespace AngularTestBackendServer.Core.Models;

public sealed class QuerySettings<T>
{
    public int Page { get; internal set; }
    public int PageSize { get; internal set; }
    public bool IncludeTotalCount { get; internal set; }
    
    public string? SearchText { get; internal set; }
    
    // TODO Validation
    public string? SortColumn { get; internal set; }
    
    // TODO enum asc or desc
    public string? SortDirection { get; internal set; }

    public ICollection<Filter>? Filters { get; internal set; }
    
    // TODO Validation
    public ICollection<string>? SelectFields { get; internal set; }
    
    public Specification<T> Specification { get; internal set; } = Specification<T>.All;
}