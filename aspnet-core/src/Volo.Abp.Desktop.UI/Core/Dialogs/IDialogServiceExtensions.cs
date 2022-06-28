using System;
using System.Threading.Tasks;

namespace Volo.Abp.Desktop.Ui.Dialogs
{
    /// <summary>
    /// Extensions for the IDialogService
    /// </summary>
    public static class IDialogServiceExtensions
    {
        /// <summary>
        /// Shows a non-modal dialog.
        /// </summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        public static void Show(this IDialogService dialogService, Type control)
        {
            dialogService.Show(control, new DialogParameters(), null);
        }

        /// <summary>
        /// Shows a non-modal dialog.
        /// </summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        public static void Show(this IDialogService dialogService, Type control, Action<IDialogResult> callback)
        {
            dialogService.Show(control, new DialogParameters(), callback);
        }

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        public static void ShowDialog(this IDialogService dialogService, Type control)
        {
            dialogService.ShowDialog(control, new DialogParameters(), null);
        }

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="dialogService">The DialogService</param>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        public static void ShowDialog(this IDialogService dialogService, Type control, Action<IDialogResult> callback)
        {
            dialogService.ShowDialog(control, new DialogParameters(), callback);
        }

        /// <summary>
        /// 弹出消息窗口
        /// </summary>
        /// <param name="dialogService"></param>
        /// <param name="title">标题</param>
        /// <param name="message">提示消息</param>
        public static void ShowMessage(this IDialogService dialogService, string title, string message)
        {
            DialogParameters parameters = new DialogParameters();
            parameters.Add("Title", title);
            parameters.Add("Message", message);

            dialogService.ShowDialog(typeof(UI.Views.Dashboard), parameters, callback => { });
        }

        /*

        /// <summary>
        /// 询问窗口
        /// </summary>
        /// <param name="hostDialogService"></param>
        /// <param name="message">提示消息</param>
        /// <param name="IdentifierName">会话ID</param>
        /// <returns></returns>
        public static async Task<bool> Question(this IHostDialogService hostDialogService, string message, string IdentifierName = AppCommonConsts.RootIdentifier)
        {
            return await Question(hostDialogService, Local.Localize("AreYouSure"), message, IdentifierName);
        }

        /// <summary>
        /// 询问窗口-指定标题
        /// </summary>
        /// <param name="hostDialogService"></param>
        /// <param name="title">标题</param>
        /// <param name="message">提示消息</param>
        /// <param name="IdentifierName">会话ID</param>
        /// <returns></returns>
        public static async Task<bool> Question(this IHostDialogService hostDialogService,
            string title,
            string message,
            string IdentifierName = AppCommonConsts.RootIdentifier)
        {
            DialogParameters param = new DialogParameters();
            param.Add("Title", title);
            param.Add("Message", message);

            var dialogResult = await hostDialogService.ShowDialogAsync(AppViewManager.HostMessageBox, param, IdentifierName);

            return dialogResult.Result == ButtonResult.OK;
        }

        /// <summary>
        /// 询问窗口
        /// </summary>
        /// <param name="dialogService"></param>
        /// <param name="title">标题</param>
        /// <param name="message">提示消息</param>
        /// <returns></returns>
        public static bool Question(this IDialogService dialogService, string title, string message)
        {
            if (string.IsNullOrWhiteSpace(title))
                title = Local.Localize("AreYouSure");

            DialogParameters parameters = new DialogParameters();
            parameters.Add("Title", title);
            parameters.Add("Message", message);

            bool dialogResult = false;
            dialogService.ShowDialog(typeof(UI.Views.Dashboard), parameters, callback =>
            {
                dialogResult = callback.Result == ButtonResult.OK;
            });
            return dialogResult;
        }

        */
    }
}