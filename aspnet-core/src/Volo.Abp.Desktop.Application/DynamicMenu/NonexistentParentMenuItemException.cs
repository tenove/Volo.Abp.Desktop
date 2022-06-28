using Volo.Abp;

namespace Volo.Abp.Desktop.DynamicMenu
{
    public sealed class NonexistentParentMenuItemException : BusinessException
    {
        public NonexistentParentMenuItemException(string parentName)
            : base("EasyAbp.Abp.DynamicMenu:NonexistentParentMenuItem")
        {
            Data["ParentName"] = parentName;
        }
    }
}