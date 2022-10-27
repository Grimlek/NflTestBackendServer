namespace AngularTestBackendServer.Data.Entities;

public class ScheduleEntity
{
    public int Id { get; set; }
    public int SeasonId { get; set; }
    public string Week { get; set; }
    public bool IsPlayoffGame { get; set; }
    
    public SeasonEntity Season { get; set; }
    public ICollection<GameEntity> WeeklyGames { get; set; }
}