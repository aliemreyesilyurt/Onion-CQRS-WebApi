﻿using WebApi.Application.Interfaces.Repositories;
using WebApi.Domain.Common;

namespace WebApi.Application.Interfaces.UnitOfWorks
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IReadRepository<T> GetReadRepository<T>() where T : class, IBaseEntity, new();
        IWriteRepository<T> GetWriteRepository<T>() where T : class, IBaseEntity, new();
        Task<int> SaveAsync();
        int Save();
    }
}