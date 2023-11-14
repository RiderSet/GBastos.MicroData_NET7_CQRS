using GBastos.MicroData.Application.EventSourcedNormalizers;
using GBastos.MicroData.Application.Interfaces;
using GBastos.MicroData.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NetDevPack.Identity.Authorization;

namespace GBastos.MicroData.Services.API.Controllers
{
    [Authorize]
    public class ItemController : ApiController
    {
        private readonly IItemAppService _itemAppService;

        public ItemController(IItemAppService itemAppService)
        {
            _itemAppService = itemAppService;
        }

        [AllowAnonymous]
        [HttpGet("item-management")]
        public async Task<IEnumerable<ItemViewModel>> Get()
        {
            return await _itemAppService.GetAll();
        }

        [AllowAnonymous]
        [HttpGet("item-management/{id:guid}")]
        public async Task<ItemViewModel> Get(Guid id)
        {
            return await _itemAppService.GetById(id);
        }

        [CustomAuthorize("Itens", "Write")]
        [HttpPost("item-management")]
        public async Task<IActionResult> Post([FromBody]ItemViewModel itemViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _itemAppService.Register(itemViewModel));
        }

        [CustomAuthorize("Itens", "Write")]
        [HttpPut("Item-management")]
        public async Task<IActionResult> Put([FromBody]ItemViewModel itemViewModel)
        {
            return !ModelState.IsValid ? CustomResponse(ModelState) : CustomResponse(await _itemAppService.Update(itemViewModel));
        }

        [CustomAuthorize("Itens", "Remove")]
        [HttpDelete("Item-management")]
        public async Task<IActionResult> Delete(Guid id)
        {
            return CustomResponse(await _itemAppService.Remove(id));
        }

        [AllowAnonymous]
        [HttpGet("Item-management/history/{id:guid}")]
        public async Task<IList<ItemHistoryData>> History(Guid id)
        {
            return await _itemAppService.GetAllHistory(id);
        }
    }
}
