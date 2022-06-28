using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Desktop.UI.Core.Threading;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public class RoleViewModel : AppViewModel
    {
        public RoleViewModel(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }
    }
}