using Autoglass.Application.Base;
using Autoglass.Domain.DTO;
using Autoglass.Domain.Models;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Autoglass.Application
{
    public interface IProdutoApp : IAppBaseCRUD<Produto, int>
    {
        Task<List<Produto>> GetAllPagination(QueryProdutoDTO query);
        Task<List<Produto>> GetByNameAsync(string name);
    }
}
