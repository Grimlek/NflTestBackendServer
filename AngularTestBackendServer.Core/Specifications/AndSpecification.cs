namespace AngularTestBackendServer.Core.Specifications;

/// <summary>
/// Generic specification class to chain/join specification using the 'and' logical operator
/// </summary>
/// <typeparam name="T"></typeparam>
internal sealed class AndSpecification<T> : Specification<T>
{
    private readonly Specification<T> _leftSpecification;
    private readonly Specification<T> _rightSpecification;

    public AndSpecification(Specification<T> leftSpecification, Specification<T> rightSpecification)
    {
        _rightSpecification = rightSpecification;
        _leftSpecification = leftSpecification;
    }

    public override string ToFilterString()
    {
        var leftFilter = _leftSpecification.ToFilterString();
        var rightFilter = _rightSpecification.ToFilterString();
        
        return $"{leftFilter} and {rightFilter}";
    }
}