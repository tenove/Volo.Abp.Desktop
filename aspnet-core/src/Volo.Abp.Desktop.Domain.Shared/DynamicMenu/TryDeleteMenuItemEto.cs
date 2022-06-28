using System;
using JetBrains.Annotations;
using Volo.Abp.ObjectExtending;

namespace Volo.Abp.Desktop.DynamicMenu
{
    [Serializable]
    public class TryDeleteMenuItemEto : ExtensibleObject
    {
        [NotNull]
        public string Name { get; set; }

        public TryDeleteMenuItemEto([NotNull] string name)
        {
            Name = name;
        }
    }
}