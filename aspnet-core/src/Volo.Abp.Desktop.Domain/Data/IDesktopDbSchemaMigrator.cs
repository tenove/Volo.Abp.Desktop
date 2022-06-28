using System.Threading.Tasks;

namespace Volo.Abp.Desktop.Data;

public interface IDesktopDbSchemaMigrator
{
    Task MigrateAsync();
}
