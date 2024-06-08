using Autoglass.Domain.DTO;
using Autoglass.Domain.Models;
using Autoglass.Service.Base;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Service
{
    public interface IProdutoService : IServiceBaseCRUD<Produto, int>
    {
        public Task<List<Produto>> GetAllPagination(string name, PaginationDTO pagination);
        Task<List<Produto>> GetByNameAsync(string name);
    }
}
