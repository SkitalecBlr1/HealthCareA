using System.Linq.Expressions;

namespace HealthCare.Specifications
{
    public class Specification<T> : ISpecification<T>
    {
        public Expression<Func<T, bool>> Criteria { get; protected set; }

        public Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
    }
}
