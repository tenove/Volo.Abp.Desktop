using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Desktop.EntityFrameworkCore;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Volo.Abp.Desktop.DynamicMenu
{
    public class MenuItemRepository : EfCoreRepository<IDesktopDbContext, MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(IDbContextProvider<IDesktopDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public override async Task<IQueryable<MenuItem>> WithDetailsAsync()
        {
            return (await base.WithDetailsAsync()).IncludeDetails();
        }
    }
}