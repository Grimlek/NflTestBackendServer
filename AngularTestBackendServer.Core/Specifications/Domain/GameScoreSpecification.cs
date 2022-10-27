using AngularTestBackendServer.Core.Enums;
using AngularTestBackendServer.Core.Models.Indexes;

namespace AngularTestBackendServer.Core.Specifications.Domain;

internal sealed class GameScoreSpecification : Specification<SeasonIndex>
{
    private readonly int _score;
    private readonly ComparisonOperator _comparisonOperator;
    
    public GameScoreSpecification(int score, ComparisonOperator comparisonOperator)
    {
        _score = score;
        _comparisonOperator = comparisonOperator;
    }
    
    public override string ToFilterString()
    {
        var comparisonOperator = "gt";

        if (_comparisonOperator == ComparisonOperator.LessThan)
        {
            comparisonOperator = "lt";
        }
        
        return $"({nameof(SeasonIndex.HomeTeamScore)} {comparisonOperator} {_score} " 
                    + $"or {nameof(SeasonIndex.AwayTeamScore)} {comparisonOperator} {_score})";
    }
}