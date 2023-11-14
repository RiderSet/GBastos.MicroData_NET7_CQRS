using GBastos.MicroData.Application.EventSourcedNormalizers;
using GBastos.MicroData.Application.Interfaces;
using GBastos.MicroData.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Authorization;

namespace GBastos.MicroData.Services.API.Controllers
{
    [Authorize]
    public class PedidoController : ApiController
    {
        private readonly IPedidoAppService _pedidoAppService;

        public PedidoController(IPedidoAppService pedidoAppService)
        {
            _pedidoAppService = pedidoAppService;
        }

        [AllowAnonymous]
        [HttpGet("pedido-management")]
        public async Task<IEnumerable<PedidoViewModel>> Get()
        {
            return await _pedidoAppService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("pedido-management/{id:guid}")]
        public async Task<PedidoViewModel> Get(Guid id)
        {
            return await _pedidoAppService.GetById(id);
        }

        [CustomAuthorize("pedidos", "Write")]
        [HttpPost("pedido-management")]
        public async Task<IActionResult> Post([FromBody]PedidoViewModel pedidoViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _pedidoAppService.Register(pedidoViewModel));
        }

        [CustomAuthorize("pedidos", "Write")]
        [HttpPut("pedido-management")]
        public async Task<IActionResult> Put([FromBody]PedidoViewModel pedidoViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _pedidoAppService.Update(pedidoViewModel));
        }

        [CustomAuthorize("pedidos", "Remove")]
        [HttpDelete("pedido-management")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _pedidoAppService.Remove(id));
        }

        [AllowAnonymous]
        [HttpGet("pedido-management/history/{id:guid}")]
        public async Task<IList<PedidoHistoryData>> History(Guid id)
        {
            return await _pedidoAppService.GetAllHistory(id);
        }
    }
}
