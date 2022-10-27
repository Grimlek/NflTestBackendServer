namespace AngularTestBackendServer.Data.Entities;

public class ConferenceEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<TeamEntity> Teams { get; set; }
}