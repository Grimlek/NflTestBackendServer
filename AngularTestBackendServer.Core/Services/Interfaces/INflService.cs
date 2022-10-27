using AngularTestBackendServer.Core.Enums;
using AngularTestBackendServer.Core.Models;
using AngularTestBackendServer.Core.Models.Filters;

namespace AngularTestBackendServer.Core.Services.Interfaces;

public interface INflService
{
    Task<ICollection<Team>> GetTeamsAsync(Search searchParams);
    Task<ICollection<Season>> GetSeasonsAsync(Search searchParams);
    Task<ICollection<Division>> GetDivisionsAsync();
    Task<ICollection<string>> GetWeeksAsync();
}