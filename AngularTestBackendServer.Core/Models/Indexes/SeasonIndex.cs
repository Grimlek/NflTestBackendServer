namespace AngularTestBackendServer.Core.Models.Indexes;

public class SeasonIndex
{
    public int SeasonId { get; set; }
    public int Year { get; set; }
    public int ScheduleId { get; set; }
    public string ScheduleWeek { get; set; }
    public bool IsPlayoffGame { get; set; }
    public string GameId { get; set; }
    public int HomeTeamId { get; set; }
    public string HomeTeamName { get; set; }
    public string HomeTeamDivision { get; set; }
    public int? HomeTeamScore { get; set; }
    public int AwayTeamId { get; set; }
    public string AwayTeamName { get; set; }
    public string AwayTeamDivision { get; set; }
    public int? AwayTeamScore { get; set; }
    public bool WasStadiumNeutral { get; set; }
    public int? WeatherTemperature { get; set; }
    public int? WeatherWindMph { get; set; }
    public int? WeatherHumidity { get; set; }
    public DateTimeOffset GameDate { get; set; }
    public int StadiumId { get; set; }
    public string StadiumName { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
}