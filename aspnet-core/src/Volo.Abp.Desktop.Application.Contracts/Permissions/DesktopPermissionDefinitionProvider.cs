using Volo.Abp.Desktop.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Volo.Abp.Desktop.Permissions;

public class DesktopPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DesktopPermissions.GroupName, L("Permission:DynamicMenu"));

        var menuItemPermission = myGroup.AddPermission(DesktopPermissions.MenuItem.Default, L("Permission:MenuItem"));
        menuItemPermission.AddChild(DesktopPermissions.MenuItem.Create, L("Permission:Create"));
        menuItemPermission.AddChild(DesktopPermissions.MenuItem.Update, L("Permission:Update"));
        menuItemPermission.AddChild(DesktopPermissions.MenuItem.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DesktopResource>(name);
    }
}