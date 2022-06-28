using System;
using Volo.Abp.ObjectExtending;

namespace Volo.Abp.Desktop.DynamicMenu
{
    [Serializable]
    public class TryCreateMenuItemEto : ExtensibleObject, IMenuItem
    {
        public string ParentName { get; set; }

        public string Name { get; set; }

        public string DisplayName { get; set; }

        public string Icon { get; set; }

        public string PageTag { get; set; }

        public string Permission { get; set; }

        public TryCreateMenuItemEto(string parentName, string name, string displayName, string icon, string tag, string permission)
        {
            ParentName = parentName;
            Name = name;
            DisplayName = displayName;
            Icon = icon;
            PageTag = tag;
            Permission = permission;
        }
    }
}