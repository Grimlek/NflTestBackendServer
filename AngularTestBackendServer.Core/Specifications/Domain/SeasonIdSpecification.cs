using AngularTestBackendServer.Core.Models.Indexes;

namespace AngularTestBackendServer.Core.Specifications.Domain;

internal sealed class SeasonIdSpecification : Specification<SeasonIndex>
{
    private readonly int _seasonId;
    
    public SeasonIdSpecification(int seasonId)
    {
        _seasonId = seasonId;
    }
    
    public override string ToFilterString()
    {
        return $"{nameof(SeasonIndex.SeasonId)} eq {_seasonId}";
    }
}