using WebApi.Domain.Common;

namespace WebApi.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> : IRepository<T>
        where T : class, IBaseEntity, new()
    {
        Task AddAsync(T entity);
        Task AddRangeAsync(IList<T> entities);
        Task<T> UpdateAsync(T entity);
        Task HardDeleteAsync(T entity);
        Task HardDeleteRangeAsync(IList<T> entities);
    }
}
