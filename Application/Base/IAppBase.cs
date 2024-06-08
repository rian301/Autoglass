using Autoglass.Domain.Core.Models;

namespace Autoglass.Application.Base
{
    public interface IAppBase
    {
    }

    public interface IAppBase<TEntity, TKey> where TEntity : Entity<TEntity, TKey>
    {

    }

    public interface IAppBase<TEntity> where TEntity : Entity<TEntity>
    {

    }
}
