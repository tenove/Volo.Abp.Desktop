using System.Threading.Tasks;
using System;
using Microsoft.Extensions.Logging;
using Volo.Abp.Desktop.UI.Core.Threading;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public class DynamicPropertyViewModel : AppViewModel
    {
        public DynamicPropertyViewModel(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }
    }
}