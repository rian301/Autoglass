using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using FluentValidation.Results;
using Autoglass.Domain.Core.Notifications;

namespace Autoglass.Infra.CrossCutting.Common.Json
{
    public class JsonRetornoDefault
    {

        #region Properties
        public List<JsonRetornoErro> Errors { get; private set; }
        #endregion

        #region Constructors
        public JsonRetornoDefault(string mensagem)
        {
            this.Errors = new List<JsonRetornoErro>();
            this.Errors.Add(new JsonRetornoErro(mensagem));
        }

        public JsonRetornoDefault(JsonRetornoErro erro)
        {
            this.Errors = new List<JsonRetornoErro>();
            if (erro != null) this.Errors.Add(erro);
        }

        public JsonRetornoDefault(IList<JsonRetornoErro> erros)
        {
            this.Errors = new List<JsonRetornoErro>();
            if (erros != null) this.Errors = erros.ToList();
        }

        public JsonRetornoDefault(IList<ValidationFailure> erros)
        {
            this.Errors = new List<JsonRetornoErro>();
            if (erros != null) setErrosPorValidationFailure(erros.ToList());
        }

        public JsonRetornoDefault(IList<DomainNotification> erros)
        {
            this.Errors = new List<JsonRetornoErro>();
            if (erros != null) setErrosPorNotification(erros.ToList());
        }
        #endregion

        #region Methods
        public string ToJsonString()
        {
            return JsonConvert.SerializeObject(this);
        }

        private void setErrosPorValidationFailure(List<ValidationFailure> erros)
        {
            foreach (var erro in erros)
            {
                this.Errors.Add(new JsonRetornoErro(erro.PropertyName, erro.ErrorMessage));
            }
        }

        private void setErrosPorNotification(List<DomainNotification> erros)
        {
            foreach (var erro in erros)
            {
                this.Errors.Add(new JsonRetornoErro(erro.Key, erro.Value));
            }
        }
        #endregion

    }
}
