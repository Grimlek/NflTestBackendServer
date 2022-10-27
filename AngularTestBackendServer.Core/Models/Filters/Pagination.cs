namespace AngularTestBackendServer.Core.Models.Filters;

public class Pagination
{
    public const int DefaultPageSize = 25;
    public const int MaxPageSize = 1000;
    
    public int Page { get; set; }
    
    private int _pageSize;
    public int PageSize
    {
        get
        {
            var pageSize = _pageSize switch
            {
                0 => DefaultPageSize,
                > MaxPageSize => MaxPageSize,
                _ => _pageSize
            };
            
            return pageSize;
        }
        
        set => _pageSize = value;
    }
    
    public bool? IncludeTotalCount { get; set; }
}