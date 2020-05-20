using System.Threading;
using System.Threading.Tasks;

namespace SmartShop.Domain.Data
{
    public interface IRepository<TAggragate> where TAggragate : class, IAggregateRoot
    {
        void Save(TAggragate aggragate);
        Task SaveAsync(TAggragate aggragate, CancellationToken cancellationToken = default);
        void Delete(TAggragate aggragate);
        Task DeleteAsync(TAggragate aggragate);
    }

    public interface IRepository<TAggragate, in TKey> : IRepository<TAggragate> where TAggragate : class, IAggregateRoot
    {
        TAggragate Get(TKey id);
        Task<TAggragate> GetAsync(TKey id, CancellationToken cancellationToken = default);
    }
}
