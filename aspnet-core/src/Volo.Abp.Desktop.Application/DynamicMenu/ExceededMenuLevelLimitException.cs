using Volo.Abp;

namespace Volo.Abp.Desktop.DynamicMenu
{
    public sealed class ExceededMenuLevelLimitException : BusinessException
    {
        public ExceededMenuLevelLimitException(int maxLevel)
            : base("EasyAbp.Abp.DynamicMenu:ExceededMenuLevelLimit")
        {
            Data["MaxLevel"] = maxLevel;
        }
    }
}