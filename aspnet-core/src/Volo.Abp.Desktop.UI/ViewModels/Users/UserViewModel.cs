using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System;
using Microsoft.Extensions.Logging;
using Volo.Abp.Desktop.UI.Core.Threading;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.Desktop.UI.Dialogs;
using System.Threading;
using System.Security.Claims;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public class UserViewModel : AppPagerViewModel
    {
        private readonly IIdentityUserAppService _userAppService;
        private readonly IIdentityRoleAppService _roleAppService;
        private readonly IAccountAppService accountService;

        public UserViewModel(IAbpLazyServiceProvider abpLazyServiceProvider, IIdentityUserAppService userAppService, IIdentityRoleAppService roleAppService) : base(abpLazyServiceProvider)
        {
            _userAppService = userAppService;
            _roleAppService = roleAppService;

            IsAdvancedFilter = false;
            input = new GetIdentityUsersInput
            {
                Filter = "",
                MaxResultCount = 10,
                SkipCount = 0
            };

            AdvancedCommand = new RelayCommand(() => { IsAdvancedFilter = !IsAdvancedFilter; });
            SelectedCommandAsync = new AsyncRelayCommand(SelectedPermissionAsync);
            SearchCommand = new RelayCommand(SearchUser);
            ResetCommand = new RelayCommand(Reset);
            PageChangedCommand = new AsyncRelayCommand(PageIndexChanged);
        }

        #region Command

        public IRelayCommand AdvancedCommand { get; private set; }
        public IAsyncRelayCommand SelectedCommandAsync { get; private set; }
        public IRelayCommand SearchCommand { get; private set; }
        public IRelayCommand ResetCommand { get; private set; }

        #endregion Command

        public GetIdentityUsersInput input { get; set; }

        /// <summary>
        ///
        /// </summary>
        private int _total = 100;

        public int Total
        {
            get => _total;
            set => SetProperty(ref _total, value);
        }

        /// <summary>
        /// 高级筛选
        /// </summary>
        private bool isAdvancedFilter;

        public bool IsAdvancedFilter
        {
            get { return isAdvancedFilter; }
            set
            {
                isAdvancedFilter = value;

                FilerTitle = value ? "△ " + "隐藏高级筛选" : "▽ " + "显示高级筛选";
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// 筛选标题文本: 收缩/展开
        /// </summary>
        private string filterTitle = string.Empty;

        public string FilerTitle
        {
            get { return filterTitle; }
            set { filterTitle = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 已选择权限的文本
        /// </summary>
        private string selectPermissions = string.Empty;

        public string SelectPermissions
        {
            get { return selectPermissions; }
            set { selectPermissions = value; OnPropertyChanged(); }
        }

        /// <summary>
        /// 更新选中的权限筛选文本
        /// </summary>
        /// <param name="count"></param>
        private void UpdateTitle(int count = 0)
        {
            SelectPermissions = "选择权限" + $"({count})";
        }

        #region Command Action

        /// <summary>
        /// 选择权限
        /// </summary>
        private async Task SelectedPermissionAsync()
        {
            //DialogParameters param = new DialogParameters();
            //param.Add("Value", flatPermission);
            //var dialogResult = await dialog.ShowDialogAsync(AppViewManager.SelectedPermission, param);
            //if (dialogResult.Result ==ButtonResult.OK)
            //{
            //    var selectedPermissions = dialogResult.Parameters.GetValue<List<string>>("Value");

            //    input.Permissions = selectedPermissions;
            //    UpdateTitle(selectedPermissions.Count);
            //    await RefreshAsync();
            //}
        }

        /// <summary>
        /// 搜索用户
        /// </summary>
        public void SearchUser()
        {
        }

        /// <summary>
        /// 重置筛选条件
        /// </summary>
        private void Reset()
        {
            // SelectedRole = null;
            // FilterText = string.Empty;

            UpdateTitle(0);
        }

        /// <summary>
        ///
        /// </summary>
        private async Task PageIndexChanged()
        {
            var identity = new ClaimsIdentity();
            identity.AddClaim(new Claim("role", "admin"));
            Thread.CurrentPrincipal = new ClaimsPrincipal(identity);

            await SetBusyAsync(async () =>
            {
                input.SkipCount = (PageIndex - 1) * PageSize;
                input.MaxResultCount = PageSize;
                var result = await _userAppService.GetListAsync(input);
                SetPagerList(result);
            });
        }

        #endregion Command Action
    }
}