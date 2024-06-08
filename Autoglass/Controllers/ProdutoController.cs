using AutoMapper;
using Autoglass.API.Controllers.Base;
using Autoglass.Application;
using Autoglass.Domain.Core.Notifications;
using Autoglass.Domain.Interfaces;
using Autoglass.Domain.Models;
using Autoglass.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;
using Autoglass.Domain.DTO;
using System.Linq;
using EO.WebBrowser;
using System.Security.Permissions;
using Dufry.Domain.Enums;

namespace Autoglass.Controllers
{
    public class ProdutoController : ApiBaseController
    {
        #region Properties
        private readonly IMapper _mapper;
        private readonly IProdutoApp _app;
        #endregion

        #region Constructors
        public ProdutoController(ILogErroApp logErroApp, IUser user, INotificationHandler<DomainNotification> notification,
                                   IProdutoApp app, IMapper mapper) : base(logErroApp, user, notification)
        {
            _mapper = mapper;
            _app = app;
        }

        #endregion

        #region Actions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProdutoViewModel>>> GetAll()
        {
            var obj = await _app.GetAllAsync();
            return ResolveReturn(_mapper.Map<IEnumerable<ProdutoViewModel>>(obj));
        }

        [HttpGet, Route("pagination")]
        public async Task<ActionResult<IEnumerable<PaginationResponseDTO<ProdutoViewModel>>>> GetAll([FromQuery] QueryProdutoDTO query)
        {
            var count = new List<Produto>();

            if (query.Description != null && query.Description != "null")
                count = await _app.GetByNameAsync(query.Description);
            else
                count = await _app.GetAllAsync();

            var obj = await _app.GetAllPagination(query);

            var objVM = _mapper.Map<List<ProdutoViewModel>>(obj);
            var ret = new PaginationResponseDTO<ProdutoViewModel>(count.Count(), objVM);
            return ResolveReturn(ret);
        }

        [HttpGet, Route("{id:int}")]
        public async Task<ActionResult<ProdutoViewModel>> Get(int id)
        {
            var obj = await _app.GetByIdAsync(id);

            if(obj == null)
            {
                return NotFound("Não encontrado.");
            }

            var objVM = _mapper.Map<ProdutoViewModel>(obj);

            return ResolveReturn(objVM);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Post([FromBody] ProdutoViewModel model)
        {
            if (!ModelState.IsValid) return BadRequestModelState();

            var obj = _mapper.Map<Produto>(model);

            var result = await _app.AddAsync(obj);

            return ResolveReturn(result);
        }

        [HttpPut, Route("{id:int}")]
        public async Task<ActionResult<ProdutoViewModel>> Put(int id, [FromBody] ProdutoViewModel model)
        {
            if (!ModelState.IsValid) return BadRequestModelState();

            model.Id = id;
            var obj = _mapper.Map<Produto>(model);
            obj.ChangedAt = System.DateTime.Now;

            await _app.UpdateAsync(obj);

            return ResolveReturn(Ok());
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<List<ProdutoViewModel>>> Remove([FromRoute] int id)
        {
            var obj = await _app.GetByIdAsync(id);
            obj.SetSituacalChange(id);
            var result = await _app.UpdateAsync(obj);
            return ResolveReturn(Ok());
        }
        #endregion
    }
}
