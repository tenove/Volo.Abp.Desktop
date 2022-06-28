using System;
using System.Collections.Generic;
using Volo.Abp.Application.Dtos;

namespace Volo.Abp.Desktop.DynamicMenu
{
    [Serializable]
    public class MenuItemDto : ExtensibleEntityDto
    {
        public string ParentName { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Icon { get; set; }

        public string PageTag { get; set; }

        public string Permission { get; set; }

        public List<MenuItemDto> MenuItems { get; set; }
    }
}