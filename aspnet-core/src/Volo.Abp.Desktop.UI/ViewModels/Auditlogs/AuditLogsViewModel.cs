using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Desktop.UI.Core.Threading;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public class AuditLogsViewModel : AppViewModel
    {
        public AuditLogsViewModel(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }
    }
}