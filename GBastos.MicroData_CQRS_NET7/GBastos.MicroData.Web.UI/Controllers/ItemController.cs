using GBastos.MicroData.Application.Interfaces;
using GBastos.MicroData.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Authorization;

namespace GBastos.MicroData.UI.Web.Controllers
{
    [Authorize]
    public class ItemController : BaseController
    {
        private readonly IItemAppService _ItemAppService;

        public ItemController(IItemAppService ItemAppService)
        {
            _ItemAppService = ItemAppService;
        }
        [AllowAnonymous]
        [HttpGet("Item-management/list-all")]
        public async Task<IActionResult> Index()
        {
            return View(await _ItemAppService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("Item-management/Item-details/{id:guid}")]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();

            var ItemViewModel = await _ItemAppService.GetById(id.Value);

            if (ItemViewModel == null) return NotFound();

            return View(ItemViewModel);
        }

        [CustomAuthorize("Items", "Write")]
        [HttpGet("Item-management/register-new")]
        public IActionResult Create()
        {
            return View();
        }

        [CustomAuthorize("Items", "Write")]
        [HttpPost("Item-management/register-new")]
        public async Task<IActionResult> Create(PedidoViewModel ItemViewModel)
        {
            if (!ModelState.IsValid) return View(ItemViewModel);
            
            if (ResponseHasErrors(await _ItemAppService.Register(ItemViewModel)))
                return View(ItemViewModel);

            ViewBag.Sucesso = "Item Registered!";

            return View(ItemViewModel);
        }

        [CustomAuthorize("Items", "Write")]
        [HttpGet("Item-management/edit-Item/{id:guid}")]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();

            var ItemViewModel = await _ItemAppService.GetById(id.Value);

            if (ItemViewModel == null) return NotFound();

            return View(ItemViewModel);
        }

        [CustomAuthorize("Items", "Write")]
        [HttpPost("Item-management/edit-Item/{id:guid}")]
        public async Task<IActionResult> Edit(PedidoViewModel ItemViewModel)
        {
            if (!ModelState.IsValid) return View(ItemViewModel);
            
            if (ResponseHasErrors(await _ItemAppService.Update(ItemViewModel)))
                return View(ItemViewModel);

            ViewBag.Sucesso = "Item Updated!";

            return View(ItemViewModel);
        }

        [CustomAuthorize("Items", "Remove")]
        [HttpGet("Item-management/remove-Item/{id:guid}")]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();

            var ItemViewModel = await _ItemAppService.GetById(id.Value);

            if (ItemViewModel == null) return NotFound();

            return View(ItemViewModel);
        }

        [CustomAuthorize("Items", "Remove")]
        [HttpPost("Item-management/remove-Item/{id:guid}"), ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (ResponseHasErrors(await _ItemAppService.Remove(id)))
                return View(await _ItemAppService.GetById(id));

            ViewBag.Sucesso = "Item Removed!";
            return RedirectToAction("Index");
        }

        [AllowAnonymous]
        [HttpGet("Item-management/Item-history/{id:guid}")]
        public async Task<JsonResult> History(Guid id)
        {
            var ItemHistoryData = await _ItemAppService.GetAllHistory(id);
            return Json(ItemHistoryData);
        }
    }
}
