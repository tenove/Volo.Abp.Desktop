using Volo.Abp.Modularity;

namespace Volo.Abp.Desktop;

[DependsOn(
    typeof(DesktopApplicationModule),
    typeof(DesktopDomainTestModule)
    )]
public class DesktopApplicationTestModule : AbpModule
{

}
