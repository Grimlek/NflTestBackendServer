using AngularTestBackendServer.Core.Models;
using AngularTestBackendServer.Core.Models.Filters;
using AngularTestBackendServer.Core.Models.Results;

namespace AngularTestBackendServer.Core.Services.Interfaces;

public interface ISearchService
{
    Task<PaginatedResult<ICollection<Season>>> SearchSeasonsAsync(Search searchParams);
    Task<PaginatedResult<ICollection<Team>>> SearchTeamsAsync(Search searchParams);
}