using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace Volo.Abp.Desktop.DynamicMenu
{
    public class MenuItem : AggregateRoot, IMenuItem
    {
        public virtual string ParentName { get; protected set; }

        [Key]
        public virtual string Name { get; protected set; }

        public virtual string DisplayName { get; protected set; }

        public virtual string Icon { get; protected set; }

        public virtual string PageTag { get; protected set; }

        public virtual string Permission { get; protected set; }

        [ForeignKey(nameof(ParentName))]
        public virtual List<MenuItem> MenuItems { get; protected set; }

        public override object[] GetKeys()
        {
            return new object[] { Name };
        }

        protected MenuItem()
        {
            MenuItems = new List<MenuItem>();
        }

        public MenuItem(
            string parentName,
            string name,
            string displayName,
            string icon,
            string tag,
            string permission,
            List<MenuItem> menuItems
        )
        {
            ParentName = parentName;
            Name = name;
            DisplayName = displayName;
            Icon = icon;
            PageTag = tag;
            Permission = permission;
            MenuItems = menuItems ?? new List<MenuItem>();
        }
    }
}