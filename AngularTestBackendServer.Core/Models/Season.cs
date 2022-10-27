namespace AngularTestBackendServer.Core.Models;

public class Season
{
    public int Id { get; set; }
    public int Year { get; set; }
    public ICollection<Schedule> WeeklySchedules { get; set; }
}