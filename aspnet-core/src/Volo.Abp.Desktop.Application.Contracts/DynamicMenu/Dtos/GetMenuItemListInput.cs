using Volo.Abp.Application.Dtos;

namespace Volo.Abp.Desktop.DynamicMenu
{
    public class GetMenuItemListInput : PagedAndSortedResultRequestDto
    {
        public string ParentName { get; set; }
    }
}