using AngularTestBackendServer.Core.Models.Indexes;

namespace AngularTestBackendServer.Core.Specifications.Domain;

internal sealed class WeeksSpecification : Specification<SeasonIndex>
{
    private readonly string _week;
    
    public WeeksSpecification(string week)
    {
        _week = week;
    }
    
    public override string ToFilterString()
    {
        return $"{nameof(SeasonIndex.ScheduleWeek)} eq '{_week}'";
    }
}