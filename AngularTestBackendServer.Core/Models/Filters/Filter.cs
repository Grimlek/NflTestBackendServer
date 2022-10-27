using AngularTestBackendServer.Core.Enums;

namespace AngularTestBackendServer.Core.Models.Filters;

public class Filter
{
    public string Field { get; set; }
    public LogicalOperator? LogicalOperator { get; set; }
    public ComparisonOperator? ComparisonOperator { get; set; }
    public string Value { get; set; }
}