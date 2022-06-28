using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Auditing;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.BackgroundWorkers;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Desktop.EntityFrameworkCore;
using Volo.Abp.Desktop.Ui.Services;
using Volo.Abp.Modularity;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.Mvvm.Services;

namespace Volo.Abp.Desktop.UI;

[DependsOn(typeof(AbpAutofacModule),
    typeof(DesktopApplicationModule),
    typeof(DesktopEntityFrameworkCoreModule))]
public class DesktopWpfModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure();
        AddServices(context.Services);
    }


    /// <summary>
    /// 
    /// </summary>
    private void Configure()
    {
        Configure<AbpAuditingOptions>(options => options.IsEnabled = false);
        Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        Configure<AbpBackgroundWorkerOptions>(options => options.IsEnabled = false);
    }

    /// <summary>
    /// 
    /// </summary>
    private void AddServices(IServiceCollection services) 
    {
        services.AddSingleton<IThemeService, ThemeService>();
        services.AddSingleton<ITaskBarService, TaskBarService>();
        services.AddSingleton<INotifyIconService, NotifyIconService>();
        services.AddSingleton<IPageService, PageService>();
        services.AddSingleton<INavigationService, NavigationService>();
    }


}
