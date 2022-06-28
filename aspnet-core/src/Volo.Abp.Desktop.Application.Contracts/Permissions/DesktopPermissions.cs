using Volo.Abp.Reflection;

namespace Volo.Abp.Desktop.Permissions;

public static class DesktopPermissions
{
    public const string GroupName = "Desktop";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DesktopPermissions));
    }

    public class MenuItem
    {
        public const string Default = GroupName + ".MenuItem";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}