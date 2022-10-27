namespace AngularTestBackendServer.Core.Specifications;

public abstract class Specification<T>
{
    public static readonly Specification<T> All = new AllSpecification<T>();
    
    public abstract string ToFilterString();
    
    public Specification<T> And(Specification<T> specification)
    {
        if (this == All)
            return specification;
        
        if (specification == All)
            return this;
        
        return new AndSpecification<T>(this, specification);
    }
    
    public Specification<T> Or(Specification<T> specification)
    {
        if (this == All || specification == All)
            return All;
        
        return new OrSpecification<T>(this, specification);
    }
    
    public Specification<T> Not()
    {
        return new NotSpecification<T>(this);
    }
    
    public static Specification<T> operator &(Specification<T> lhs, Specification<T> rhs) => lhs.And(rhs);
    public static Specification<T> operator |(Specification<T> lhs, Specification<T> rhs) => lhs.Or(rhs);
    public static Specification<T> operator !(Specification<T> spec) => spec.Not();
}







