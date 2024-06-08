using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Autoglass.Application;
using Autoglass.Domain.Core.Notifications;
using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using Autoglass.Infra.CrossCutting.Common.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Autoglass.Infra.Framework.Web.Controller
{
    [Produces("application/json")]
    [Route("[controller]")]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {

        #region Properties
        protected readonly ILogErroApp _logErroApp;
        protected readonly IUser _user;
        protected readonly INotificationHandler<DomainNotification> _notification;
        #endregion

        #region Constructors
        public BaseController(ILogErroApp logErroApp, IUser user, INotificationHandler<DomainNotification> notification)
        {
            _logErroApp = logErroApp;
            _user = user;
            _notification = notification;
        }
        #endregion

        #region Methods
        protected BadRequestObjectResult BadRequestRegraNegocio(string mensagem) => BadRequest(new JsonRetornoDefault(mensagem));

        protected BadRequestObjectResult BadRequestRegraNegocio(IList<JsonRetornoErro> erros) => BadRequest(new JsonRetornoDefault(erros));

        protected BadRequestObjectResult BadRequestRegraNegocio(IList<ValidationFailure> erros) => BadRequest(new JsonRetornoDefault(erros));

        protected BadRequestObjectResult BadRequestRegraNegocio(IList<DomainNotification> erros) => BadRequest(new JsonRetornoDefault(erros));


        protected BadRequestObjectResult BadRequestNaoIdentificado(Exception ex, object model = null)
        {
            try
            {
                var classe = ex.TargetSite.DeclaringType.FullName + "." + ex.TargetSite.Name;
                var dados = (model != null ? JsonConvert.SerializeObject(model) : null);
                _logErroApp.Insert(new LogErro(1, classe, ex.ToString(), (_user != null ? JsonConvert.SerializeObject(_user) : null), dados));
            }
            catch (Exception)
            { }

            return BadRequest(new JsonRetornoDefault(new JsonRetornoErro(ex.Message)));
        }

        protected BadRequestObjectResult BadRequestModelState()
        {
            IList<JsonRetornoErro> errosRetorno = new List<JsonRetornoErro>();

            var erros = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var erro in erros)
            {
                var erroMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                errosRetorno.Add(new JsonRetornoErro(erroMsg));
            }

            return BadRequest(new JsonRetornoDefault(errosRetorno));
        }

        protected ObjectResult ResolveReturn(object model)
        {
            if (_notification.HasNotifications())
                return BadRequestRegraNegocio(_notification.GetNotifications());
            else
                return Ok(model);
        }
        #endregion

    }
}
