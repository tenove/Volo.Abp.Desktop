using Volo.Abp.Desktop.UI.Core.Threading;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public class OrganizationsViewModel : AppViewModel
    {
        public OrganizationsViewModel(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }
    }
}