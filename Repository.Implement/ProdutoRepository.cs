using Microsoft.EntityFrameworkCore;
using Autoglass.Domain.Core.Notifications;
using Autoglass.Domain.Models;
using Autoglass.Infra.Data.Context;
using Autoglass.Repository.Implement.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autoglass.Domain.DTO;
using Domain.Models;
using Dufry.Domain.Enums;

namespace Repository.Implement
{
    public class ProdutoRepository : RepositoryBaseCRUD<Produto, int>, IProdutoRepository
    {
        public ProdutoRepository(ApplicationDbContext context, INotificationHandler<DomainNotification> notification) : base(context, notification)
        {
        }

        public override Task<List<Produto>> GetAllAsync()
        {
            return dbSet.Where(x => x.Situacao == SituacaoProduto.Ativo).ToListAsync();
        }

        public override Task<Produto> GetByIdAsync(int id)
        {
            return dbSet.Where(x => x.Id == id && x.Situacao == SituacaoProduto.Ativo).FirstOrDefaultAsync();
        }

        public Task<List<Produto>> GetAllPagination(string descricao, PaginationDTO pagination)
        {
            if (descricao != "null" && descricao != null)
                return dbSet
                    .Where(x => x.Descricao.Contains(descricao) && x.Situacao == SituacaoProduto.Ativo)
                    .Skip(pagination.Page * pagination.Limit)
                    .Take(pagination.Limit)
                    .OrderBy(x => x.Descricao)
                    .ToListAsync();
            else
                return dbSet.Skip(pagination.Page * pagination.Limit).Take(pagination.Limit).OrderBy(x => x.Descricao).ToListAsync();
        }

        public Task<List<Produto>> GetByNameAsync(string description)
        {
            return dbSet.Where(x => x.Descricao.Contains(description) && x.Situacao == SituacaoProduto.Ativo).ToListAsync();
        }
    }
}
