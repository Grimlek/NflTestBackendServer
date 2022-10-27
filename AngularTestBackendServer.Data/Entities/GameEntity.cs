namespace AngularTestBackendServer.Data.Entities;

public class GameEntity
{
    public int Id { get; set; }
    public int ScheduleId { get; set; }
    public int HomeTeamId { get; set; }
    public int AwayTeamId { get; set; }
    public int? HomeTeamScore { get; set; }
    public int? AwayTeamScore { get; set; }
    public int StadiumId { get; set; }
    public bool WasStadiumNeutral { get; set; }
    public int? WeatherTemperature { get; set; }
    public int? WeatherWindMph { get; set; }
    public int? WeatherHumidity { get; set; }
    public DateTime GameDate { get; set; }

    public TeamEntity HomeTeam { get; set; }
    public TeamEntity AwayTeam { get; set; }
    public StadiumEntity Stadium { get; set; }
    public ScheduleEntity Schedule { get; set; }
}