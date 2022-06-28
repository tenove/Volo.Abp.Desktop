using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Desktop.Data;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Desktop.EntityFrameworkCore;

public class EntityFrameworkCoreDesktopDbSchemaMigrator
    : IDesktopDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreDesktopDbSchemaMigrator(
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the DesktopDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<DesktopDbContext>()
            .Database
            .MigrateAsync();
    }
}
