using Autoglass.Application.Implement.Base;
using Autoglass.Domain.Core.Notifications;
using Autoglass.Domain.DTO;
using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using Autoglass.Service;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Application.Implement
{
    public class ProdutoApp : AppBaseCRUD<Produto, int>, IProdutoApp
    {
        #region Properties
        private readonly IProdutoService _service;
        #endregion

        #region Constructors
        public ProdutoApp(IProdutoService service, INotificationHandler<DomainNotification> notification, IUser user, IUnitOfWork uow) : base(service, notification, user, uow)
        {
            _service = service;
        }
        #endregion

        public Task<List<Produto>> GetByNameAsync(string name) => _service.GetByNameAsync(name);

        public async Task<List<Produto>> GetAllPagination(QueryProdutoDTO query)
        {
            var pageOptions = new PaginationDTO(query.PageIndex, query.PageSize);
            var result = await _service.GetAllPagination(query.Description, pageOptions);
            return result;
        }
    }
}
