using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Identity;
using IdentityUser = Volo.Abp.Identity.IdentityUser;
using Volo.Abp.Uow;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public class LoginWindowViewModel : AppDialogViewModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IdentityUserManager _identityUserManager;

        public LoginWindowViewModel(IAbpLazyServiceProvider abpLazyServiceProvider, SignInManager<IdentityUser> signInManager, IdentityUserManager identityUserManager) : base(abpLazyServiceProvider)
        {
            _signInManager = signInManager;
            _identityUserManager = identityUserManager;
            LoginCommand = new AsyncRelayCommand(LoginAsync);
        }

        public LoginWindowViewModel() : base()
        {
        }

        public IAsyncRelayCommand LoginCommand { get; }

        /// <summary>
        /// 租户名称
        /// </summary>
        private string tenantName;

        public string TenantName
        {
            get => tenantName;
            set => SetProperty(ref tenantName, value);
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

        /// <summary>
        /// login action
        /// </summary>
        /// <returns></returns>
        private async Task LoginAsync()
        {
            await SetBusyAsync(async () =>
            {
                using (var uow = UnitOfWorkManager.Begin())
                {
                    var user = await _identityUserManager.FindByNameAsync(UserName);
                    var signInResult = await _signInManager.CheckPasswordSignInAsync(user, Password, false);
                    if (signInResult.Succeeded)
                    {
                        var principal = await _signInManager.CreateUserPrincipalAsync(user);
                        Thread.CurrentPrincipal = principal;
                        await LoginUserSuccessed();
                    }
                }
            });
        }

        private async Task LoginUserSuccessed()
        {
            //清理
            UserName = string.Empty;
            Password = string.Empty;

            OnDialogClosed();
            await Task.CompletedTask;
        }
    }
}