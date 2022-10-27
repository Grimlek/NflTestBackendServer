namespace AngularTestBackendServer.Data.Entities;

public class SeasonEntity
{
    public int Id { get; set; }
    public int Year { get; set; }

    public ICollection<ScheduleEntity> WeeklySchedules { get; set; }
}