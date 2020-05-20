using Microsoft.EntityFrameworkCore;
using SmartShop.Domain;
using SmartShop.Domain.Data;
using System.Threading;
using System.Threading.Tasks;

namespace SmartShop.EntityFramework
{
    public abstract class Repository<TAggragate> : IRepository<TAggragate> where TAggragate : class, IAggregateRoot
    {
        protected virtual DbContext DbContext { get; set; }

        public Repository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual void Delete(TAggragate aggragate)
        {
            DbContext.Remove(aggragate);
        }

        public virtual async Task DeleteAsync(TAggragate aggragate)
        {
            DbContext.Remove(aggragate);
            await Task.Delay(0);
        }

        public void Save(TAggragate aggragate)
        {
            DbContext.Add(aggragate);
        }

        public virtual async Task SaveAsync(TAggragate aggragate, CancellationToken cancellationToken = default)
        {
            await DbContext.AddAsync(aggragate, cancellationToken);
        }
    }

    public abstract class Repository<TAggragate, TKey> : Repository<TAggragate>, IRepository<TAggragate, TKey> where TAggragate : class, IAggregateRoot
    {
        public Repository(DbContext dbContext) : base(dbContext)
        {
        }

        public virtual TAggragate Get(TKey id)
        {
            return DbContext.Find<TAggragate>(id);
        }

        public virtual async Task<TAggragate> GetAsync(TKey id, CancellationToken cancellationToken = default)
        {
            return await DbContext.FindAsync<TAggragate>(id);
        }
    }
}
