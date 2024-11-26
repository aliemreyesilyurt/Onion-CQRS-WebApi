using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using WebApi.Application.Interfaces.Repositories;
using WebApi.Domain.Common;
using WebApi.Persistence.Context;

namespace WebApi.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IBaseEntity, new()
    {
        private readonly DbContext _context;

        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
        {
            Table.AsNoTracking();
            if (predicate is not null)
                Table.Where(predicate);

            return Table.CountAsync();
        }

        public IQueryable<T> Find(Expression<Func<T, bool>> predicate, bool enableTracking = false)
        {
            if (!enableTracking)
                Table.AsNoTracking();

            return Table.Where(predicate);
        }

        public async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>,
            IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false)
        {
            IQueryable<T> query = Table;
            if (!enableTracking)
                query = query.AsNoTracking();
            if (include is not null)
                query = include(query);
            if (predicate is not null)
                query = query.Where(predicate);
            if (orderBy is not null)
                return await orderBy(query)
                    .ToListAsync();

            return await query.ToListAsync();
        }

        public async Task<IList<T>> GetAllByPagingAsync(Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            bool enableTracking = false,
            int currentPage = 1,
            int pageSize = 3)
        {
            IQueryable<T> query = Table;
            if (!enableTracking)
                query = query.AsNoTracking();
            if (include is not null)
                query = include(query);
            if (predicate is not null)
                query = query.Where(predicate);
            if (orderBy is not null)
                return await orderBy(query)
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

            return await query
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool enableTracking = false)
        {
            IQueryable<T> query = Table;
            if (!enableTracking)
                query = query.AsNoTracking();
            if (include is not null)
                query = include(query);

            //query.Where(predicate);

            return await query.FirstOrDefaultAsync(predicate);
        }
    }
}
