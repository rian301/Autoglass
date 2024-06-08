using Autoglass.Domain.Core.Models;

namespace Autoglass.Repository.Base
{
    public interface IRepositoryBase
    {
    }

    public interface IRepositoryBase<TEntity, TKey> where TEntity : Entity<TEntity, TKey>
    {

    }

    public interface IRepositoryBase<TEntity> where TEntity : Entity<TEntity>
    {

    }
}
