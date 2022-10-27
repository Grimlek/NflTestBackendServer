namespace AngularTestBackendServer.Core.Models;

public class Game
{
    public int GameId { get; set; }
    public DateTimeOffset GameDate { get; set; }
    public int? HomeTeamScore { get; set; }
    public int? AwayTeamScore { get; set; }
    public bool WasStadiumNeutral { get; set; }
    public int? WeatherHumidity { get; set; }
    public int? WeatherTemperature { get; set; }
    public int? WeatherWindMph { get; set; }
    public Team HomeTeam { get; set; }
    public Team AwayTeam { get; set; }
    public Stadium Stadium { get; set; }
}