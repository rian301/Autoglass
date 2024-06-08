using Autoglass.Domain.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Autoglass.Repository.Base
{
    public interface IRepositoryBaseCRUD<TEntity, TKey> : IDisposable where TEntity : Entity<TEntity, TKey>
    {
        void Add(TEntity obj);
        Task<TEntity> GetByIdAsync(TKey id);
        TEntity GetById(TKey id);
        IEnumerable<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        IQueryable<TEntity> Table();
        IQueryable<TEntity> TableNoTracking();
        void Update(TEntity obj);
        void Delete(TKey id);
        void DeleteAll();
    }
}
