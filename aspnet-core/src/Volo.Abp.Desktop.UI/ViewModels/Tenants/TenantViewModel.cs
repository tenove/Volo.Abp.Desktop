using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Volo.Abp.Desktop.UI.Core.Threading;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public class TenantViewModel : AppViewModel
    {
        public TenantViewModel(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }
    }
}