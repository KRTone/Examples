namespace Abstractions
{
    public interface IReadOnlyRepository<TEntity> where TEntity : class
    {
        TEntity[] GetAll();
        TEntity GetById(object id);
        TEntity[] Filter(IExpressionSpecification<TEntity> specification);
    }
}
