using Autoglass.Domain.Core.Notifications;
using Autoglass.Domain.DTO;
using Autoglass.Domain.Models;
using Autoglass.Repository;
using Autoglass.Service.Implement.Base;
using Domain.Models;
using Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Service.Implement
{
    public class ProdutoService : ServiceBaseCRUD<Produto, int>, IProdutoService
    {
        #region Properties
        private readonly IProdutoRepository _repository;
        #endregion

        public ProdutoService(IProdutoRepository repository, INotificationHandler<DomainNotification> notification)
            : base(repository, notification)
        {
            _repository = repository;
        }

        public Task<List<Produto>> GetAllPagination(string name, PaginationDTO pagination) => _repository.GetAllPagination(name, pagination);
        public Task<List<Produto>> GetByNameAsync(string name) => _repository.GetByNameAsync(name);
    }
}
