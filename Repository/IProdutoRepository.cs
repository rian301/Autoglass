using Autoglass.Domain.DTO;
using Autoglass.Repository.Base;
using Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Repository
{
    public interface IProdutoRepository : IRepositoryBaseCRUD<Produto, int>
    {
        Task<List<Produto>> GetAllPagination(string name, PaginationDTO pagination);
        Task<List<Produto>> GetByNameAsync(string name);
    }
}
