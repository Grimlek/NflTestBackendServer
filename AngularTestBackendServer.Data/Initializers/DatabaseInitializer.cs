using AngularTestBackendServer.Data.Csv;
using AngularTestBackendServer.Data.Entities;

namespace AngularTestBackendServer.Data.Initializers;

public static class DatabaseInitializer
{
    public static async Task InitializeAsync(NflDbContext dbContext)
    {
        await dbContext.Database.EnsureCreatedAsync();
        
        if (dbContext.Teams.Any() == false)
        {
            var teams = await CsvFileProcessor.GeNflTeamsAsync();
            await dbContext.Teams.AddRangeAsync(teams);

            await dbContext.SaveChangesAsync();
        }
        
        if (dbContext.Stadiums.Any() == false)
        {
            var stadiums = await CsvFileProcessor.GetNflStadiumsAsync();
            await dbContext.Stadiums.AddRangeAsync(stadiums);

            await dbContext.SaveChangesAsync();
        }
        
        if (dbContext.Seasons.Any() == false)
        {
            var seasons = await CsvFileProcessor.GetNflSeasonsAsync();

            var dbSeasons = new List<SeasonEntity>();

            foreach (var season in seasons)
            {
                dbSeasons.Add(new SeasonEntity
                {
                    Year = season.Year
                });
            }
            
            await dbContext.Seasons.AddRangeAsync(dbSeasons);
            await dbContext.SaveChangesAsync();
            
            foreach (var season in seasons)
            {
                var seasonId = dbSeasons.Single(e => e.Year == season.Year).Id;
                var dbWeeklySchedules = new List<ScheduleEntity>();
                
                foreach (var schedule in season.WeeklySchedules)
                {
                    schedule.SeasonId = seasonId;
                    
                    dbWeeklySchedules.Add(new ScheduleEntity
                    {
                        SeasonId = seasonId,
                        Week = schedule.Week,
                        IsPlayoffGame = schedule.IsPlayoffGame
                    });
                }
                
                await dbContext.Schedules.AddRangeAsync(dbWeeklySchedules);
            }
            
            await dbContext.SaveChangesAsync();

            var stadiums = dbContext.Stadiums.ToList();
            var teams = dbContext.Teams.ToList();
            var schedules = dbContext.Schedules.ToList();
            
            foreach (var season in seasons)
            {
                var seasonId = dbSeasons.Single(e => e.Year == season.Year).Id;
                
                foreach (var schedule in season.WeeklySchedules)
                {
                    var dbGames = new List<GameEntity>();
                    
                    foreach (var game in schedule.WeeklyGames)
                    {
                        dbGames.Add(new GameEntity
                        {
                            ScheduleId = schedules.Single(e => e.SeasonId == seasonId && e.Week == schedule.Week).Id,
                            GameDate = game.GameDate,
                            WeatherHumidity = game.WeatherHumidity,
                            WeatherTemperature = game.WeatherTemperature,
                            WeatherWindMph = game.WeatherWindMph,
                            AwayTeamScore = game.AwayTeamScore,
                            HomeTeamScore = game.HomeTeamScore,
                            WasStadiumNeutral = game.WasStadiumNeutral,
                            StadiumId = stadiums.First(e => e.Name == game.Stadium.Name).Id,
                            HomeTeamId = teams.First(e => e.Name == game.HomeTeam.Name).Id,
                            AwayTeamId = teams.First(e => e.Name == game.AwayTeam.Name).Id
                        });
                    }
                    
                    await dbContext.Games.AddRangeAsync(dbGames);
                }
            }
            
            await dbContext.SaveChangesAsync();
        }
    }
}