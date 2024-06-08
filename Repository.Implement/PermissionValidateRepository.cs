using Autoglass.Domain.Core.Notifications;
using Autoglass.Infra.Data.Context;
using Autoglass.Repository.Implement.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autoglass.Repository.Implement
{
    public class PermissionValidateRepository : RepositoryBase, IPermissionValidateRepository
    {

        #region Constructors
        public PermissionValidateRepository(ApplicationDbContext context, INotificationHandler<DomainNotification> notification) : base(context, notification)
        {
        }
        #endregion

        #region Methods

        public bool ValidateUserPermission(int userId, string[] constPermission)
        {
            var result = (from a in Context.UserProfilePermission
                          join b in Context.User on a.UserProfileId equals b.UserProfileId
                          join c in Context.Permission on a.PermissionId equals c.Id
                          where constPermission.Contains(c.ConstPermission)
                          where b.Id == userId
                          select a).AsNoTracking().Count();
            return Convert.ToBoolean(result);
        }

        public bool ValidateUserPermission(int userId, string constPermission)
        {
            return ValidateUserPermission(userId, new string[] { constPermission });
        }
        #endregion
    }
}
