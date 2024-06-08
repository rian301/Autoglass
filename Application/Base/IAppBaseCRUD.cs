using Autoglass.Domain.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Application.Base
{
    public interface IAppBaseCRUD<TEntity, TKey> : IAppBase<TEntity, TKey> where TEntity : Entity<TEntity, TKey>
    {
        Task<TEntity> GetByIdAsync(TKey id);
        IEnumerable<TEntity> GetAll();
        Task<List<TEntity>> GetAllAsync();
        Task<bool> AddAsync(TEntity obj);
        Task<bool> UpdateAsync(TEntity obj);
        Task<bool> DeleteAsync(TKey id);
        Task<bool> DeleteAllAsync();
    }
}
