using WebApi.Domain.Common;

namespace WebApi.Application.Interfaces.Repositories
{
    public interface IWriteRepository<T> : IRepository<T>
        where T : class, IBaseEntity, new()
    {
    }
}
