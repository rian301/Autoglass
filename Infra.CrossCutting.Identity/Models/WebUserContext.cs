using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Autoglass.Domain.Core.Consts;
using System.Text.Json.Serialization;
using Autoglass.Domain.Interfaces;

namespace Autoglass.Infra.CrossCutting.Identity.Models
{
    [Serializable]
    public class WebUserContext : IUser
    {

        #region Propriedades
        private readonly IHttpContextAccessor _accessor;
        #endregion

        #region Construtores
        public WebUserContext(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        #endregion

        [JsonIgnore]
        public IEnumerable<Claim> Claims => _accessor.HttpContext.User.Claims.ToList();
        public int? Id { get { return IsAuthenticated ? (int?)Convert.ToInt32(Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.ID_USER).Value) : null; } }
        public string Name { get { return Claims.FirstOrDefault(x => x.Type == "name")?.Value; } }
        public bool IsAuthenticated { get {
                if (_accessor == null || _accessor.HttpContext == null) return false;
                return _accessor.HttpContext.User.Identity.IsAuthenticated; 
            } }
        public string Email { get { return IsAuthenticated ? Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value : null; } }
        public int? CustomerId { get { return IsAuthenticated && Claims.Any(x => x.Type == CustomClaimTypes.ID_CUSTOMER) ? (int?)Convert.ToInt32(Claims.FirstOrDefault(x => x.Type == CustomClaimTypes.ID_CUSTOMER).Value) : null; } }
    }
}
