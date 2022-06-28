using Volo.Abp.Desktop.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace Volo.Abp.Desktop;

[DependsOn(
    typeof(DesktopEntityFrameworkCoreTestModule)
    )]
public class DesktopDomainTestModule : AbpModule
{

}
