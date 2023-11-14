using GBastos.MicroData.Application.Interfaces;
using GBastos.MicroData.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Authorization;

namespace GBastos.MicroData.UI.Web.Controllers
{
    [Authorize]
    public class PedidoController : BaseController
    {
        private readonly IItemAppService _PedidoAppService;

        public PedidoController(IItemAppService PedidoAppService)
        {
            _PedidoAppService = PedidoAppService;
        }
        [AllowAnonymous]
        [HttpGet("Pedido-management/list-all")]
        public async Task<IActionResult> Index()
        {
            return View(await _PedidoAppService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("Pedido-management/Pedido-details/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var PedidoViewModel = await _PedidoAppService.GetById(id.Value);

            if (PedidoViewModel == null) return NotFound();

            return View(PedidoViewModel);
        }

        [CustomAuthorize("Pedidos", "Write")]
        [HttpGet("Pedido-management/register-new")]
        public IActionResult Create()
        {
            return View();
        }

        [CustomAuthorize("Pedidos", "Write")]
        [HttpPost("Pedido-management/register-new")]
        public async Task<IActionResult> Create(PedidoViewModel PedidoViewModel)
        {
            if (!ModelState.IsValid) return View(PedidoViewModel);
            
            if (ResponseHasErrors(await _PedidoAppService.Register(PedidoViewModel)))
                return View(PedidoViewModel);

            ViewBag.Sucesso = "Pedido Registered!";

            return View(PedidoViewModel);
        }

        [CustomAuthorize("Pedidos", "Write")]
        [HttpGet("Pedido-management/edit-Pedido/{id:guid}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var PedidoViewModel = await _PedidoAppService.GetById(id.Value);

            if (PedidoViewModel == null) return NotFound();

            return View(PedidoViewModel);
        }

        [CustomAuthorize("Pedidos", "Write")]
        [HttpPost("Pedido-management/edit-Pedido/{id:guid}")]
        public async Task<IActionResult> Edit(PedidoViewModel PedidoViewModel)
        {
            if (!ModelState.IsValid) return View(PedidoViewModel);
            
            if (ResponseHasErrors(await _PedidoAppService.Update(PedidoViewModel)))
                return View(PedidoViewModel);

            ViewBag.Sucesso = "Pedido Updated!";

            return View(PedidoViewModel);
        }

        [CustomAuthorize("Pedidos", "Remove")]
        [HttpGet("Pedido-management/remove-Pedido/{id:guid}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var PedidoViewModel = await _PedidoAppService.GetById(id.Value);

            if (PedidoViewModel == null) return NotFound();

            return View(PedidoViewModel);
        }

        [CustomAuthorize("Pedidos", "Remove")]
        [HttpPost("Pedido-management/remove-Pedido/{id:guid}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (ResponseHasErrors(await _PedidoAppService.Remove(id)))
                return View(await _PedidoAppService.GetById(id));

            ViewBag.Sucesso = "Pedido Removed!";
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet("Pedido-management/Pedido-history/{id:guid}")]
        public async Task<JsonResult> History(Guid id)
        {
            var PedidoHistoryData = await _PedidoAppService.GetAllHistory(id);
            return Json(PedidoHistoryData);
        }
    }
}
