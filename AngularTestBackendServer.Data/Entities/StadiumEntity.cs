namespace AngularTestBackendServer.Data.Entities;

public class StadiumEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? StadiumTypeId { get; set; }
    public int? StadiumOpenYear { get; set; }
    public int? StadiumCloseYear { get; set; }
    public int? Capacity { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public string? Country { get; set; }

    public ICollection<GameEntity> Games { get; set; }
}