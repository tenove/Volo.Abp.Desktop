using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Desktop;

[Dependency(ReplaceServices = true)]
public class DesktopBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Desktop";
}
