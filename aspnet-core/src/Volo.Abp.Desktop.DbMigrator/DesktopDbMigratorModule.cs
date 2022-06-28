using Volo.Abp.Desktop.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace Volo.Abp.Desktop.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(DesktopEntityFrameworkCoreModule),
    typeof(DesktopApplicationContractsModule)
    )]
public class DesktopDbMigratorModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
    }
}
