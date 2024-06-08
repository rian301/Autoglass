using Autoglass.Domain.Core.Models;

namespace Autoglass.Service.Base
{
    public interface IServiceBase
    {
    }

    public interface IServiceBase<TEntity, TKey> where TEntity : Entity<TEntity, TKey>
    {

    }

    public interface IServiceBase<TEntity> where TEntity : Entity<TEntity>
    {

    }
}
