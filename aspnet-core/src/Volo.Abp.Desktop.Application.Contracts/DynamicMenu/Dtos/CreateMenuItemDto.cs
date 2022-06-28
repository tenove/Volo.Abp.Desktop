using System;
using Volo.Abp.ObjectExtending;

namespace Volo.Abp.Desktop.DynamicMenu
{
    [Serializable]
    public class CreateMenuItemDto : ExtensibleObject
    {
        public string ParentName { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Icon { get; set; }

        public string PageTag { get; set; }
        public string Permission { get; set; }
    }
}