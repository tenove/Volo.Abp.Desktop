using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Desktop.Data;

/* This is used if database provider does't define
 * IDesktopDbSchemaMigrator implementation.
 */
public class NullDesktopDbSchemaMigrator : IDesktopDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
