using AngularTestBackendServer.Core.Enums;
using AngularTestBackendServer.Core.Models;

namespace AngularTestBackendServer.Core.Repositories;

public interface INflRepository
{
    Task<ICollection<Team>> GetTeamsAsync(QuerySettings<Team> querySettings);
    Task<ICollection<Season>> GetSeasonsAsync(QuerySettings<Season> querySettings);
    Task<ICollection<Division>> GetDivisionsAsync();
    Task<ICollection<string>> GetWeeksAsync();
}