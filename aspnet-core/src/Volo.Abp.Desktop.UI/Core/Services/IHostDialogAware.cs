﻿using CommunityToolkit.Mvvm.Input;
using Volo.Abp.Desktop.UI.Dialogs;

namespace Volo.Abp.Desktop.UI.Services
{
    /// <summary>
    /// 对话主机ViewModel基类
    /// </summary>
    public interface IHostDialogAware
    {
        /// <summary>
        /// DialogHost顶级节点
        /// </summary>
        string IdentifierName { get; set; }

        /// <summary>
        /// 页面初始化前传递参数事件
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        void OnDialogOpened(IDialogParameters parameters);

        /// <summary>
        /// 确认
        /// </summary>
        IRelayCommand SaveCommand { get; }

        /// <summary>
        /// 取消
        /// </summary>
        IRelayCommand CancelCommand { get; }
    }
}