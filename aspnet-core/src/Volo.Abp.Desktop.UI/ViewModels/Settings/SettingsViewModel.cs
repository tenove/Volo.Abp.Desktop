using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Desktop.UI.Core.Threading;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public class SettingsViewModel : AppViewModel
    {
        public SettingsViewModel(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }
    }
}