using System;
using System.Collections.Generic;
using System.Text;

namespace Volo.Abp.Desktop
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class MenuTypeAttribute : Attribute
    {
        public Type MenuType { get; set; }
        public string Name { get; set; }

        public MenuTypeAttribute(string menuName, Type type)
        {
            this.Name = menuName;
            this.MenuType = type;
        }
    }
}