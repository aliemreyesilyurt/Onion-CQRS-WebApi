using Microsoft.EntityFrameworkCore;

namespace WebApi.Application.Interfaces.Repositories
{
    public interface IRepository<T> where T : class
    {
        DbSet<T> Table { get; }
    }
}
