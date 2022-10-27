namespace AngularTestBackendServer.Core.Specifications;

/// <summary>
/// Generic specification class to apply no filters/rules
/// </summary>
/// <typeparam name="T"></typeparam>
internal sealed class AllSpecification<T> : Specification<T>
{
    public override string ToFilterString()
    {
        return string.Empty; // no filter
    }
}