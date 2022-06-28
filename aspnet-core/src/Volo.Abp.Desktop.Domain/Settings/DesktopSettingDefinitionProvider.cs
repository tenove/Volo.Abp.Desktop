using Volo.Abp.Settings;

namespace Volo.Abp.Desktop.Settings;

public class DesktopSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(DesktopSettings.MySetting1));
    }
}
