using AngularTestBackendServer.Core.Models;
using AngularTestBackendServer.Core.Models.Indexes;
using AngularTestBackendServer.Core.Models.Results;

namespace AngularTestBackendServer.Core.Repositories;

public interface ISearchRepository
{
    Task<PaginatedResult<ICollection<Season>>> SearchSeasonsAsync(QuerySettings<SeasonIndex> querySettings);
    Task<PaginatedResult<ICollection<Team>>> SearchTeamsAsync(QuerySettings<TeamIndex> querySettings);
}