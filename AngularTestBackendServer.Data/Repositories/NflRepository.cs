using AngularTestBackendServer.Core.Enums;
using Microsoft.EntityFrameworkCore;
using AngularTestBackendServer.Core.Models;
using AngularTestBackendServer.Core.Repositories;
using AngularTestBackendServer.Data.Entities;
using AutoMapper;

namespace AngularTestBackendServer.Data.Repositories;

public sealed class NflRepository : INflRepository
{
    private readonly NflDbContext _dbContext;
    private readonly IMapper _mapper;
    
    public NflRepository(NflDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }
    
    public async Task<ICollection<Team>> GetTeamsAsync(QuerySettings<Team> querySettings)
    {
        throw new NotImplementedException();
    }
    
    public async Task<ICollection<Season>> GetSeasonsAsync(QuerySettings<Season> querySettings)
    {
        var seasonEntities = 
                await _dbContext.Seasons.AsNoTracking().OrderByDescending(e => e.Year).ToListAsync();
        var seasons = _mapper.Map<ICollection<SeasonEntity>, ICollection<Season>>(seasonEntities);
        return seasons;
    }
    
    public async Task<ICollection<Division>> GetDivisionsAsync()
    {
        var divisionEntities = await _dbContext.Divisions.AsNoTracking()
                                                                         .OrderBy(e => e.Name)
                                                                         .ToListAsync();
        
        var divisions = 
                _mapper.Map<ICollection<DivisionEntity>, ICollection<Division>>(divisionEntities);
        return divisions;
    }
    
    public async Task<ICollection<string>> GetWeeksAsync()
    {
        var weeks = await _dbContext.Schedules.AsNoTracking()
                                                        .Select(e => e.Week)
                                                        .Distinct()
                                                        .OrderBy(e => e)
                                                        .ToListAsync();
        return weeks;
    }
}