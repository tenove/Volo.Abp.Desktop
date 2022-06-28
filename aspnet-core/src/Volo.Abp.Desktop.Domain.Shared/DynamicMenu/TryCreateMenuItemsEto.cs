using System;
using System.Collections.Generic;
using Volo.Abp.ObjectExtending;

namespace Volo.Abp.Desktop.DynamicMenu
{
    [Serializable]
    public class TryCreateMenuItemsEto : ExtensibleObject
    {
        public List<TryCreateMenuItemEto> Items { get; set; }

        public TryCreateMenuItemsEto(List<TryCreateMenuItemEto> items)
        {
            Items = items;
        }
    }
}