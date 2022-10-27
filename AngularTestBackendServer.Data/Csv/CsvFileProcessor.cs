using System.Globalization;
using AngularTestBackendServer.Core.Enums;
using AngularTestBackendServer.Data.Entities;
using CsvHelper;

namespace AngularTestBackendServer.Data.Csv;

public static class CsvFileProcessor
{
    public static async Task<ICollection<TeamEntity>> GeNflTeamsAsync()
    {
        var assembly = typeof(CsvFileProcessor).Assembly;
        await using var resourceStream = assembly.GetManifestResourceStream($"AngularTestBackendServer.Data.Csv.nfl_teams.csv");

        if (resourceStream == null || resourceStream.CanRead == false)
        {
            throw new Exception("An error occurred reading csv file nfl_teams.csv. Ensure the file is copied to the build directory.");
        }
        
        using var streamReader = new StreamReader(resourceStream);
        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
            
        await csvReader.ReadAsync();
        csvReader.ReadHeader();
        
        var teams = new List<TeamEntity>();
        
        while (await csvReader.ReadAsync())
        {
            var division = csvReader.GetField<string>("team_division");

            if (string.IsNullOrEmpty(division))
            {
                division = csvReader.GetField<string>("team_division_pre2002"); 
            }
            
            int? divisionId = null;
            
            if (string.IsNullOrEmpty(division) == false)
            {
                division = division.Replace(" ", "");

                divisionId = (int) Enum.Parse(typeof(Division), division, true);
            }
            
            var conference = csvReader.GetField<string>("team_conference");
            
            var team = new TeamEntity
            {
                Name = csvReader.GetField<string>("team_name"),
                Abbreviation = csvReader.GetField<string>("team_id"),
                ShortName = csvReader.GetField<string>("team_name_short"),
                ConferenceId = (int) Enum.Parse(typeof(Conference), conference, true),
                DivisionId = divisionId
            };
            
            teams.Add(team);
        }
        
        return teams;
    }
    
    public static async Task<ICollection<StadiumEntity>> GetNflStadiumsAsync()
    {
        var assembly = typeof(CsvFileProcessor).Assembly;
        await using var resourceStream = assembly.GetManifestResourceStream($"AngularTestBackendServer.Data.Csv.nfl_stadiums.csv");
        
        if (resourceStream == null || resourceStream.CanRead == false)
        {
            throw new Exception("An error occurred reading csv file nfl_stadiums.csv. Ensure the file is copied to the build directory.");
        }
        
        using var streamReader = new StreamReader(resourceStream);
        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        
        await csvReader.ReadAsync();
        csvReader.ReadHeader();
        
        var stadiums = new List<StadiumEntity>();
        
        while (await csvReader.ReadAsync())
        {
            var location = csvReader.GetField<string>("stadium_location");
            
            string? city = null;
            string? state = null;
            string? country = null;
            
            if (string.IsNullOrEmpty(location) == false)
            {
                var locationParts = location.Split(", ");
                
                city = locationParts[0];
                
                var locationPart = locationParts[1];
                
                if (locationParts.Length > 2)
                {
                    country = locationPart;
                }
                else
                {
                    state = locationPart;
                }
            }
            
            var stadiumType = csvReader.GetField<string>("stadium_type");
            
            if (string.IsNullOrEmpty(stadiumType))
            {
                stadiumType = StadiumType.Unknown.ToString();
            }
            
            int? stadiumOpen = null;
            int? stadiumClose = null;
            int? stadiumCapacity = null;
            
            var stadiumOpenCsvValue = csvReader.GetField<string>("stadium_open");
            var stadiumCloseCsvValue = csvReader.GetField<string>("stadium_close");
            var stadiumCapacityCsvValue = csvReader.GetField<string>("stadium_capacity");
            
            if (string.IsNullOrWhiteSpace(stadiumOpenCsvValue) == false)
            {
                stadiumOpen = int.Parse(stadiumOpenCsvValue);
            }
            
            if (string.IsNullOrWhiteSpace(stadiumCloseCsvValue) == false)
            {
                stadiumClose = int.Parse(stadiumCloseCsvValue);
            }
            
            if (string.IsNullOrWhiteSpace(stadiumCapacityCsvValue) == false)
            {
                stadiumCapacity = int.Parse(stadiumCapacityCsvValue.Replace(",", ""));
            }
            
            var stadium = new StadiumEntity
            {
                Name = csvReader.GetField<string>("stadium_name"),
                City = city,
                State = state,
                Country = country,
                Capacity = stadiumCapacity,
                StadiumOpenYear = stadiumOpen,
                StadiumCloseYear = stadiumClose,
                StadiumTypeId = (int) Enum.Parse(typeof(StadiumType), stadiumType, true)
            };
            
            stadiums.Add(stadium);
        }
        
        return stadiums;
    }
    
    public static async Task<ICollection<SeasonEntity>> GetNflSeasonsAsync()
    {
        var assembly = typeof(CsvFileProcessor).Assembly;
        await using var resourceStream = assembly.GetManifestResourceStream($"AngularTestBackendServer.Data.Csv.spreadspoke_scores.csv");
        
        if (resourceStream == null || resourceStream.CanRead == false)
        {
            throw new Exception("An error occurred reading csv file spreadspoke_scores.csv. Ensure the file is copied to the build directory.");
        }
        
        using var streamReader = new StreamReader(resourceStream);
        using var csvReader = new CsvReader(streamReader, CultureInfo.InvariantCulture);
        
        await csvReader.ReadAsync();
        csvReader.ReadHeader();
        
        var seasons = new List<SeasonEntity>();
        
        while (await csvReader.ReadAsync())
        {
            var seasonYear = csvReader.GetField<int>("schedule_season");
            var season = seasons.SingleOrDefault(e => e.Year == seasonYear);
            
            if (season == null)
            {
                season = new SeasonEntity
                {
                    Year = seasonYear,
                    WeeklySchedules = new List<ScheduleEntity>()
                };
                
                seasons.Add(season);
            }

            var scheduleWeek = csvReader.GetField<string>("schedule_week");
            var schedule = season.WeeklySchedules.SingleOrDefault(e => e.Week.Equals(scheduleWeek));

            if (schedule == null)
            {
                schedule = new ScheduleEntity
                {
                    Week = scheduleWeek,
                    IsPlayoffGame = csvReader.GetField<bool>("schedule_playoff"),
                    WeeklyGames = new List<GameEntity>()
                };
                
                season.WeeklySchedules.Add(schedule);
            }

            int? homeTeamScore = null;
            int? awayTeamScore = null;
            int? weatherHumidity = null;
            int? weatherTemp = null;
            int? weatherWindMph = null;

            var homeTeamScoreCsvValue = csvReader.GetField<string>("score_home");
            var awayTeamScoreCsvValue = csvReader.GetField<string>("score_away");
            var weatherHumidityCsvValue = csvReader.GetField<string>("weather_humidity");
            var weatherTempCsvValue = csvReader.GetField<string>("weather_temperature");
            var weatherWindMphCsvValue = csvReader.GetField<string>("weather_wind_mph");

            if (string.IsNullOrWhiteSpace(weatherHumidityCsvValue) == false)
            {
                weatherHumidity = int.Parse(weatherHumidityCsvValue);
            }

            if (string.IsNullOrWhiteSpace(weatherTempCsvValue) == false)
            {
                weatherTemp = int.Parse(weatherTempCsvValue);
            }

            if (string.IsNullOrWhiteSpace(weatherWindMphCsvValue) == false)
            {
                weatherWindMph = int.Parse(weatherWindMphCsvValue);
            }

            if (string.IsNullOrWhiteSpace(homeTeamScoreCsvValue) == false)
            {
                homeTeamScore = int.Parse(homeTeamScoreCsvValue);
            }

            if (string.IsNullOrWhiteSpace(awayTeamScoreCsvValue) == false)
            {
                awayTeamScore = int.Parse(awayTeamScoreCsvValue);
            }
            
            var game = new GameEntity
            {
                HomeTeamScore = homeTeamScore,
                AwayTeamScore = awayTeamScore,
                GameDate = csvReader.GetField<DateTime>("schedule_date"),
                WeatherHumidity = weatherHumidity,
                WeatherTemperature = weatherTemp,
                WeatherWindMph = weatherWindMph,
                WasStadiumNeutral = csvReader.GetField<bool>("stadium_neutral"),
                Stadium = new StadiumEntity
                {
                    Name = csvReader.GetField<string>("stadium")
                },
                HomeTeam  = new TeamEntity
                {
                    Name = csvReader.GetField<string>("team_home")
                },
                AwayTeam = new TeamEntity
                {
                    Name = csvReader.GetField<string>("team_away")
                }
            };

            schedule.WeeklyGames.Add(game);
        }
        
        return seasons;
    }
}