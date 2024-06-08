using Microsoft.AspNetCore.Authorization;
using Autoglass.Application;
using Autoglass.Domain.Core.Consts;
using Autoglass.Domain.Core.Notifications;
using Autoglass.Domain.Interfaces;
using Autoglass.Infra.Framework.Web.Controller;

namespace Autoglass.API.Controllers.Base
{
    public class ApiBaseController : BaseController
    {
        public ApiBaseController(ILogErroApp logErroApp, IUser user, INotificationHandler<DomainNotification> notification) : base(logErroApp, user, notification)
        {
        }
    }
}
