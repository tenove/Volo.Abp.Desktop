using System;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace Volo.Abp.Desktop.DynamicMenu
{
    public interface IMenuItemAppService :
        ICrudAppService<
            MenuItemDto,
            MenuItemKey,
            GetMenuItemListInput,
            CreateMenuItemDto,
            UpdateMenuItemDto>
    {
    }
}