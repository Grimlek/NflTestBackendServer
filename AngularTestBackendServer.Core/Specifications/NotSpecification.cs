namespace AngularTestBackendServer.Core.Specifications;

/// <summary>
/// Generic specification class to chain/join specification using the 'not' logical operator
/// </summary>
/// <typeparam name="T"></typeparam>
internal sealed class NotSpecification<T> : Specification<T>
{
    private readonly Specification<T> _specification;

    public NotSpecification(Specification<T> specification)
    {
        _specification = specification;
    }

    public override string ToFilterString()
    {
        var filter = _specification.ToFilterString();
        return $"not ({filter})";
    }
}