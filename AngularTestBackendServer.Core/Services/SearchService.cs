using AngularTestBackendServer.Core.Enums;
using AngularTestBackendServer.Core.Models;
using AngularTestBackendServer.Core.Models.Filters;
using AngularTestBackendServer.Core.Models.Indexes;
using AngularTestBackendServer.Core.Models.Results;
using AngularTestBackendServer.Core.Repositories;
using AngularTestBackendServer.Core.Services.Interfaces;
using AngularTestBackendServer.Core.Specifications;
using AngularTestBackendServer.Core.Specifications.Domain;

namespace AngularTestBackendServer.Core.Services;

public sealed class SearchService : ISearchService
{
    private readonly ISearchRepository _searchRepository;
    
    public SearchService(ISearchRepository searchRepository)
    {
        _searchRepository = searchRepository;
    }
    
    public async Task<PaginatedResult<ICollection<Season>>> SearchSeasonsAsync(Search searchParams)
    {
        var specification = Specification<SeasonIndex>.All;
        
        if (searchParams.Filters != null && searchParams.Filters.Count != 0)
        {
            foreach (var filter in searchParams.Filters)
            {
                Specification<SeasonIndex> filterSpecification = filter.Field.ToLower() switch
                {
                    "teamid" => new GamesByTeamIdSpecification(Convert.ToInt32(filter.Value)),
                    "seasonid" => new SeasonIdSpecification(Convert.ToInt32(filter.Value)),
                    "weeks" => new WeeksSpecification(filter.Value),
                    "scores" => new GameScoreSpecification(Convert.ToInt32(filter.Value), filter.ComparisonOperator!.Value),
                    "divisions" => new DivisionsSpecification(filter.Value),
                    _ => throw new NotSupportedException($"Filter '{filter.Field}' is not supported.")
                };
                
                specification = filter.LogicalOperator switch
                {
                    LogicalOperator.Or => specification.Or(filterSpecification),
                    LogicalOperator.Not => specification.And(filterSpecification.Not()),
                    _ => specification.And(filterSpecification)
                };
            }
        }
        
        var querySettings = searchParams.ToQuerySettings(specification);
        var paginatedSeasons = 
                await _searchRepository.SearchSeasonsAsync(querySettings);
        
        return paginatedSeasons;
    }
    
    public async Task<PaginatedResult<ICollection<Team>>> SearchTeamsAsync(Search searchParams)
    {
        if (searchParams.Sort?.Column != null 
            && searchParams.Sort.Column.Equals("name", StringComparison.OrdinalIgnoreCase))
        {
            searchParams.Sort.Column = nameof(TeamIndex.TeamName);
        }
        
        var querySettings = searchParams.ToQuerySettings<TeamIndex>();
        
        var paginatedTeams = await _searchRepository.SearchTeamsAsync(querySettings);
        return paginatedTeams;
    }
}