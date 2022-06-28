using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Desktop.DynamicMenu;

namespace Volo.Abp.Desktop.Controllers
{
    [Route("/api/dynamic-menu/menu-item")]
    public class MenuItemController : DesktopController, IMenuItemAppService
    {
        private readonly IMenuItemAppService _service;

        public MenuItemController(IMenuItemAppService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("")]
        public virtual Task<MenuItemDto> CreateAsync(CreateMenuItemDto input)
        {
            return _service.CreateAsync(input);
        }

        [HttpPut]
        [Route("{Name}")]
        public virtual Task<MenuItemDto> UpdateAsync(MenuItemKey id, UpdateMenuItemDto input)
        {
            return _service.UpdateAsync(id, input);
        }

        [HttpDelete]
        [Route("{Name}")]
        public virtual Task DeleteAsync(MenuItemKey id)
        {
            return _service.DeleteAsync(id);
        }

        [HttpGet]
        [Route("{Name}")]
        public virtual Task<MenuItemDto> GetAsync(MenuItemKey id)
        {
            return _service.GetAsync(id);
        }

        [HttpGet]
        [Route("")]
        public virtual Task<PagedResultDto<MenuItemDto>> GetListAsync(GetMenuItemListInput input)
        {
            return _service.GetListAsync(input);
        }
    }
}