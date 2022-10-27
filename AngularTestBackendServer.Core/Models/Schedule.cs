namespace AngularTestBackendServer.Core.Models;

public class Schedule
{
    public int Id { get; set; }
    public string Week { get; set; }
    public bool IsPlayoffGame { get; set; }
    public ICollection<Game> WeeklyGames { get; set; }
}