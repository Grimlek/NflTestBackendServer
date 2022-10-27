using AngularTestBackendServer.Core.Enums;
using AngularTestBackendServer.Core.Models;
using AngularTestBackendServer.Core.Models.Filters;
using AngularTestBackendServer.Core.Repositories;
using AngularTestBackendServer.Core.Services.Interfaces;

namespace AngularTestBackendServer.Core.Services;

public sealed class NflService : INflService
{
    private readonly INflRepository _nflRepository;
    
    public NflService(INflRepository nflRepository)
    {
        _nflRepository = nflRepository;
    }
    
    public async Task<ICollection<Team>> GetTeamsAsync(Search searchParams)
    {
        // TODO Add support for pagination, sorting, expanding or including, and specifications
        searchParams.Pagination.Page = 0;
        searchParams.Pagination.PageSize = int.MaxValue;
        
        var querySettings = searchParams.ToQuerySettings<Team>();
        var teams = await _nflRepository.GetTeamsAsync(querySettings);
        return teams;
    }
    
    public async Task<ICollection<Season>> GetSeasonsAsync(Search searchParams)
    {
        // TODO Add support for pagination, sorting, expanding or including, and specifications
        searchParams.Pagination.Page = 0;
        searchParams.Pagination.PageSize = int.MaxValue;
        
        var querySettings = searchParams.ToQuerySettings<Season>();
        var seasons = await _nflRepository.GetSeasonsAsync(querySettings);
        return seasons;
    }
    
    public async Task<ICollection<Division>> GetDivisionsAsync()
    {
        var divisions = await _nflRepository.GetDivisionsAsync();
        return divisions;
    }
    
    public async Task<ICollection<string>> GetWeeksAsync()
    {
        var weeks = await _nflRepository.GetWeeksAsync();
        return weeks;
    }
}