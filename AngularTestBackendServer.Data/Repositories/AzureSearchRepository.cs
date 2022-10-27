using System.Reflection;
using AngularTestBackendServer.Core;
using AngularTestBackendServer.Core.Models;
using AngularTestBackendServer.Core.Models.Indexes;
using AngularTestBackendServer.Core.Models.Results;
using AngularTestBackendServer.Core.Repositories;
using Azure;
using Azure.Search.Documents;
using Azure.Search.Documents.Models;

namespace AngularTestBackendServer.Data.Repositories;

public sealed class AzureSearchRepository : ISearchRepository
{
    private readonly SearchClient _azureTeamsSearchClient;
    private readonly SearchClient _azureSeasonsSearchClient;
    
    public AzureSearchRepository(AppSettings.DataSourceSettings dataSourceSettings)
    {
        var azureSearchSettings = dataSourceSettings.AzureSearch;
        
        var azureSearchCredentials = new AzureKeyCredential(azureSearchSettings.ApiKey);
        var searchUri = new Uri(azureSearchSettings.Endpoint);
        
        _azureTeamsSearchClient = 
            new SearchClient(searchUri, azureSearchSettings.TeamsIndexName, azureSearchCredentials);
        _azureSeasonsSearchClient = 
            new SearchClient(searchUri, azureSearchSettings.SeasonsIndexName, azureSearchCredentials);
    }
    
    public async Task<PaginatedResult<ICollection<Season>>> SearchSeasonsAsync(QuerySettings<SeasonIndex> querySettings)
    {
        var searchOptions = new SearchOptions
        {
            SearchMode = SearchMode.Any,
            Size = querySettings.PageSize,
            Skip = querySettings.Page * querySettings.PageSize,
            IncludeTotalCount = querySettings.IncludeTotalCount,
            Filter = querySettings.Specification.ToFilterString()
        };

        if (querySettings.SelectFields != null && querySettings.SelectFields.Any())
        {
            foreach (var field in querySettings.SelectFields)
            {
                searchOptions.Select.Add(field);
            }
        }
        
        if (querySettings.SortColumn != null && string.IsNullOrEmpty(querySettings.SortColumn) == false)
        {
            var propertyInfo = typeof(SeasonIndex).GetProperty(querySettings.SortColumn, 
                                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                throw new Exception(
                    $"Invalid sort column name for searching seasons. '{querySettings.SortColumn}'");
            }
            
            searchOptions.OrderBy.Add($"{propertyInfo.Name} {querySettings.SortDirection}");
        }
        
        var searchResponse = 
                await _azureSeasonsSearchClient.SearchAsync<SeasonIndex>(querySettings.SearchText, searchOptions);
        var pagedResults = 
                searchResponse.Value.GetResults().Select(e => e.Document).ToList();
        
        var seasons = new List<Season>();
        
        foreach (var result in pagedResults)
        {
            var season = seasons.SingleOrDefault(e => e.Year == result.Year);
            
            if (season == null)
            {
                season = new Season
                {
                    Id = result.SeasonId,
                    Year = result.Year,
                    WeeklySchedules = new List<Schedule>()
                };
                
                seasons.Add(season);
            }
            
            if (result.ScheduleId == default)
            {
                continue;
            }
            
            var schedule = season.WeeklySchedules.SingleOrDefault(e => e.Id == result.ScheduleId);
            
            if (schedule == null)
            {
                schedule = new Schedule
                {
                    Id = result.ScheduleId,
                    Week = result.ScheduleWeek,
                    IsPlayoffGame = result.IsPlayoffGame,
                    WeeklyGames = new List<Game>()
                };
                
                season.WeeklySchedules.Add(schedule);
            }
            
            if (string.IsNullOrEmpty(result.GameId))
            {
                continue;
            }
            
            var convertedGameId = Convert.ToInt32(result.GameId);
            var game = schedule.WeeklyGames.SingleOrDefault(e => e.GameId == convertedGameId);
            
            if (game != null)
            {
                continue;
            }
            
            game = new Game
            {
                GameId = convertedGameId,
                HomeTeamScore = result.HomeTeamScore,
                AwayTeamScore = result.AwayTeamScore,
                HomeTeam = new Team
                {
                    Id = result.HomeTeamId,
                    Name = result.HomeTeamName,
                    Division = result.HomeTeamDivision
                },
                AwayTeam = new Team
                {
                    Id = result.AwayTeamId,
                    Name = result.AwayTeamName,
                    Division = result.AwayTeamDivision
                },
                Stadium = new Stadium
                {
                    Id = result.StadiumId,
                    Name = result.StadiumName,
                    City = result.City,
                    State = result.State,
                    Country = result.Country
                },
                GameDate = result.GameDate,
                WeatherHumidity = result.WeatherHumidity,
                WeatherTemperature = result.WeatherTemperature,
                WeatherWindMph = result.WeatherWindMph
            };
            
            schedule.WeeklyGames.Add(game);
        }
        
        var paginatedResults = new PaginatedResult<ICollection<Season>>
        {
            Results = seasons,
            Page = querySettings.Page,
            PageSize = querySettings.PageSize,
            TotalCount = searchResponse.Value.TotalCount 
        };
        
        return paginatedResults;
    }
    
    public async Task<PaginatedResult<ICollection<Team>>> SearchTeamsAsync(QuerySettings<TeamIndex> querySettings)
    {
        var searchOptions = new SearchOptions
        {
            Size = querySettings.PageSize,
            Skip = querySettings.Page * querySettings.PageSize,
            IncludeTotalCount = querySettings.IncludeTotalCount,
        };
        
        if (querySettings.SortColumn != null && string.IsNullOrEmpty(querySettings.SortColumn) == false)
        {
            var propertyInfo = typeof(TeamIndex).GetProperty(querySettings.SortColumn, 
                                BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

            if (propertyInfo == null)
            {
                throw new Exception(
                    $"Invalid sort column name for searching teams. '{querySettings.SortColumn}'");
            }
            
            searchOptions.OrderBy.Add($"{propertyInfo.Name} {querySettings.SortDirection}");
        }
        
        var searchResponse = 
                await _azureTeamsSearchClient.SearchAsync<TeamIndex>(querySettings.SearchText, searchOptions);
        var pagedResults = 
                searchResponse.Value.GetResults().Select(e => e.Document).ToList();
        
        var teams = new List<Team>();
        
        foreach (var result in pagedResults)
        {
            var team = new Team
            {
                Id = Convert.ToInt32(result.TeamId),
                Name = result.TeamName,
                ShortName = result.ShortName,
                Abbreviation = result.Abbreviation,
                Division = result.DivisionName,
                Conference = result.ConferenceName
            };
            
            teams.Add(team);
        }
        
        var paginatedResults = new PaginatedResult<ICollection<Team>>
        {
            Results = teams,
            Page = querySettings.Page,
            PageSize = querySettings.PageSize,
            TotalCount = searchResponse.Value.TotalCount 
        };
        
        return paginatedResults;
    }
}