using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public class LoginViewModel : AppDialogViewModel
    {
        public LoginViewModel(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
        }

        public LoginViewModel() : base()
        {
        }

        public IRelayCommand<string> LoginCommand { get; }

        /// <summary>
        /// 租户名称
        /// </summary>
        private string tenancyName;

        public string TenancyName
        {
            get => tenancyName;
            set => SetProperty(ref tenancyName, value);
        }

        /// <summary>
        /// 用户名
        /// </summary>
        private string userName;

        public string UserName
        {
            get => userName;
            set
            {
                userName = value;
                SetLoginButtonEnabled();
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 密码
        /// </summary>
        private string password;

        public string Password
        {
            get => password;
            set
            {
                password = value;
                SetLoginButtonEnabled();
                OnPropertyChanged();
            }
        }

        /// <summary>
        ///
        /// </summary>
        private bool isLoginEnabled;

        public bool IsLoginEnabled
        {
            get => isLoginEnabled;
            set => SetProperty(ref isLoginEnabled, value);
        }

        /// <summary>
        /// 记住我
        /// </summary>
        private bool isRememberMe;

        public bool IsRememberMe
        {
            get => isRememberMe;
            set => SetProperty(ref isRememberMe, value);
        }

        /// <summary>
        ///
        /// </summary>
        private void SetLoginButtonEnabled()
        {
            IsLoginEnabled = !string.IsNullOrWhiteSpace(UserName) && !string.IsNullOrWhiteSpace(Password);
        }
    }
}