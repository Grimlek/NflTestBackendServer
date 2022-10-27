using AngularTestBackendServer.Core.Models.Indexes;

namespace AngularTestBackendServer.Core.Specifications.Domain;

internal sealed class DivisionsSpecification : Specification<SeasonIndex>
{
    private readonly string _divisionName;
    
    public DivisionsSpecification(string divisionName)
    {
        _divisionName = divisionName;
    }
    
    public override string ToFilterString()
    {
        return $"({nameof(SeasonIndex.HomeTeamDivision)} eq '{_divisionName}' " 
                    + $"or {nameof(SeasonIndex.AwayTeamDivision)} eq '{_divisionName}')";
    }
}