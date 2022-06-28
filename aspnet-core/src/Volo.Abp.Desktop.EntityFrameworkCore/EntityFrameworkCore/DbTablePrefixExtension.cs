using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;
using Volo.Abp.SettingManagement;

namespace Volo.Abp.Desktop.EntityFrameworkCore
{
    public static class DbTablePrefixExtension
    {
        private const string DefaultTablePrefix = "m_";

        public static void AddDbTablePrefix(this ServiceConfigurationContext context) => AddDbTablePrefix();

        public static void AddDbTablePrefix()
        {
            AbpSettingManagementDbProperties.DbTablePrefix = DefaultTablePrefix;
            BackgroundJobsDbProperties.DbTablePrefix = DefaultTablePrefix;
        }
    }
}