namespace AngularTestBackendServer.Data.Entities;

public class TeamEntity
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string ShortName { get; set; }
    public string Abbreviation { get; set; }
    public int ConferenceId { get; set; }
    public int? DivisionId { get; set; }

    public ICollection<GameEntity> HomeGames { get; set; }
    public ICollection<GameEntity> AwayGames { get; set; }
    public DivisionEntity Division { get; set; }
    public ConferenceEntity Conference { get; set; }
}