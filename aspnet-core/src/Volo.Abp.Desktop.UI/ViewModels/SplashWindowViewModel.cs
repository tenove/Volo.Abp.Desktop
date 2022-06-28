using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Desktop.UI.Dialogs;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public class SplashWindowViewModel : AppDialogViewModel
    {
        public SplashWindowViewModel(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }

        public SplashWindowViewModel() : base()
        {
        }

        /// <summary>
        ///
        /// </summary>
        private string displayText;

        public string DisplayText
        {
            get => displayText;
            set => SetProperty(ref displayText, value);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameters"></param>
        public override async void OnDialogOpened(IDialogParameters parameters)
        {
            await SetBusyAsync(async () =>
            {
                //加载本地的缓存信息
                DisplayText = "正在加载应用程序";

                await Task.Delay(1000);

                //加载系统资源
                DisplayText = "正在加载系统资源";

                OnDialogClosed(ButtonResult.No);
            });
        }
    }
}