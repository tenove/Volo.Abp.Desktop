using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.Desktop.DynamicMenu;
using Volo.Abp.EntityFrameworkCore;

namespace Volo.Abp.Desktop.EntityFrameworkCore
{
    [ConnectionStringName("Default")]
    public interface IDesktopDbContext : IEfCoreDbContext
    {
        DbSet<MenuItem> MenuItems { get; set; }
    }
}