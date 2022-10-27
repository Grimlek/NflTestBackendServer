using AngularTestBackendServer.Core.Specifications;

namespace AngularTestBackendServer.Core.Models.Filters;

/// <summary>
/// Interface model for searching with text, pagination, sorting, filtering and selection of fields.
/// </summary>
public class Search
{
    public string? SearchText { get; set; }

    public Pagination Pagination { get; set; } = new Pagination();
    public Sort? Sort { get; set; }
    public LinkedList<Filter>? Filters { get; set; }
    
    public ICollection<string>? SelectFields { get; set; }

    public QuerySettings<T> ToQuerySettings<T>(Specification<T>? specification = null)
    {
        var querySettings = new QuerySettings<T>
        {
            SearchText = $"{SearchText}*",
            Page = Pagination.Page,
            PageSize = Pagination.PageSize,
            Filters = Filters,
            SortColumn = Sort?.Column,
            SortDirection = Sort?.Direction,
            IncludeTotalCount = Pagination.IncludeTotalCount ?? false
        };

        if (specification != null)
        {
            querySettings.Specification = specification;
        }

        return querySettings;
    }
}