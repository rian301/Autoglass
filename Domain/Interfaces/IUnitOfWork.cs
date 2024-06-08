using System.Threading.Tasks;

namespace Autoglass.Domain.Interfaces
{
    public interface IUnitOfWork
    {        
        Task<bool> SaveAsync();
        bool Save();
        void BeginTransaction();
        void Commit();
        void Rollback();
        void Dispose();
    }
}
