using Abstractions;
using System.Data.Entity;
using System.Linq;

namespace Repositories
{
    public class Repository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class
    {
        private DbSet<TEntity> dbSet;

        public Repository(DbContext context)
        {
            dbSet = context.Set<TEntity>();
        }

        public TEntity[] Filter(IExpressionSpecification<TEntity> specification)
        {
            IQueryable<TEntity> entities = dbSet;

            IQueryable<TEntity> query = entities.Where(specification.ToExpression());

            return query.ToArray();
        }

        public TEntity[] GetAll()
        {
            return dbSet.ToArray();
        }

        public TEntity GetById(object id)
        {
            return dbSet.Find(id);
        }
    }
}
