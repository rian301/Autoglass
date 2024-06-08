using Autoglass.Domain.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Service.Base
{
    public interface IServiceBaseCRUD<TEntity, TKey> : IServiceBase<TEntity, TKey> where TEntity : Entity<TEntity, TKey>
    {
        void Add(TEntity obj);
        IEnumerable<TEntity> GetAll();
        void Update(TEntity obj);
        void Delete(TKey id);
        void DeleteAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<List<TEntity>> GetAllAsync();
    }
}
