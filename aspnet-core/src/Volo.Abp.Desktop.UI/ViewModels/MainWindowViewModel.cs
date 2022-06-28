using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Volo.Abp.Desktop.UI.Core.Threading;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;

using CommunityToolkit.Mvvm.Input;

using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public partial class MainWindowViewModel : AppViewModel
    {
        public MainWindowViewModel(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
            Title = "Volo Abp Desktop";
            Subtitle = "Main_Description";
            Icon = Enum.GetName(SymbolRegular.Book20);
        }

        public MainWindowViewModel() : base()
        {
            Icon = Enum.GetName(SymbolRegular.Book20);
            Title = "Volo Abp Desktop";
            Subtitle = "Subtitle";
        }

        public bool GetIsNotBusy() => IsNotBusy;
    }
}