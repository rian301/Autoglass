using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using Autoglass.Infra.Data.Context;
using Autoglass.Repository;
using Microsoft.EntityFrameworkCore;

namespace Autoglass.Repository.Implement
{
    public class LogErroRepository : ILogErroRepository
    {
        #region Properties
        private readonly DbContextOptions<ApplicationDbContext> _optionsDB;
        private readonly IUser _user;
        #endregion

        #region Constructors
        public LogErroRepository(DbContextOptions<ApplicationDbContext> options, IUser user)
        {
            _optionsDB = options;
            _user = user;
        }
        #endregion

        #region Methods
        public void Insert(LogErro erro)
        {
            using (var db = new ApplicationDbContext(_optionsDB, _user))
            {
                db.LogErro.Add(erro);
                db.SaveChanges();
            }
        }
        #endregion
    }
}
