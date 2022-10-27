namespace AngularTestBackendServer.Data.Entities;

public class DivisionEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public ICollection<TeamEntity> Teams { get; set; }
}