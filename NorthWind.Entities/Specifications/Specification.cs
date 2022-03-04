namespace NorthWind.Entities.Specifications;

public abstract class Specification<T>
{
    public abstract Expression<Func<T, bool>> ConditionExpression { get; }

    public bool IsSatisfiedBy(T entity)
    {
        Func<T, bool> expressionDelegate = ConditionExpression.Compile();
        return expressionDelegate(entity);
    }

    //bool SomeMethod(T instance)
    //{
    //    return true;        //must be return the validation result
    //}
}
