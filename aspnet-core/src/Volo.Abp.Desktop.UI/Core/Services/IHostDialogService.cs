using System;
using System.Threading.Tasks;
using Volo.Abp.Desktop.UI.Dialogs;

namespace Volo.Abp.Desktop.UI.Services
{
    /// <summary>
    /// 对话主机服务接口
    /// </summary>
    public interface IHostDialogService : IDialogService
    {
        Task<IDialogResult> ShowDialogAsync(Type control, IDialogParameters parameters = null, string IdentifierName = "Root");

        IDialogResult ShowWindow(Type control);
    }
}