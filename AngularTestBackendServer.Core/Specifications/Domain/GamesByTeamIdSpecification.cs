using AngularTestBackendServer.Core.Models.Indexes;

namespace AngularTestBackendServer.Core.Specifications.Domain;

internal sealed class GamesByTeamIdSpecification : Specification<SeasonIndex>
{
    private readonly int _teamId;
    
    public GamesByTeamIdSpecification(int teamId)
    {
        _teamId = teamId;
    }
    
    public override string ToFilterString()
    {
        return $"({nameof(SeasonIndex.HomeTeamId)} eq {_teamId} or {nameof(SeasonIndex.AwayTeamId)} eq {_teamId})";
    }
}