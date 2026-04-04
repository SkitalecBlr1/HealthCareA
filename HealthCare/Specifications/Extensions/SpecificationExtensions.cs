using System.Linq.Expressions;

namespace HealthCare.Specifications.Extensions
{
    public static class SpecificationExtensions
    {
        public static ISpecification<T> And<T>(
            this ISpecification<T> left,
            ISpecification<T> right)
        {
            var param = Expression.Parameter(typeof(T));

            var body = Expression.AndAlso(
                Expression.Invoke(left.Criteria, param),
                Expression.Invoke(right.Criteria, param)
            );

            return new Specification<T>(
                Expression.Lambda<Func<T, bool>>(body, param)
            );
        }
    }
}
