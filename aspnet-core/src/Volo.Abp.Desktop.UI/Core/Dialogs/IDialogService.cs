using System;
using Volo.Abp.DependencyInjection;

namespace Volo.Abp.Desktop.UI.Dialogs
{
    /// <summary>
    /// Interface to show modal and non-modal dialogs.
    /// </summary>
    public interface IDialogService

    {
        /// <summary>
        /// Shows a non-modal dialog.
        /// </summary>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        void Show(Type control, IDialogParameters parameters, Action<IDialogResult> callback);

        /// <summary>
        /// Shows a non-modal dialog.
        /// </summary>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <param name="windowName">The name of the hosting window registered with the IContainerRegistry.</param>
        void Show(Type control, IDialogParameters parameters, Action<IDialogResult> callback, Type dialogWindowType);

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        void ShowDialog(Type control, IDialogParameters parameters, Action<IDialogResult> callback);

        /// <summary>
        /// Shows a modal dialog.
        /// </summary>
        /// <param name="name">The name of the dialog to show.</param>
        /// <param name="parameters">The parameters to pass to the dialog.</param>
        /// <param name="callback">The action to perform when the dialog is closed.</param>
        /// <param name="windowName">The name of the hosting window registered with the IContainerRegistry.</param>
        void ShowDialog(Type control, IDialogParameters parameters, Action<IDialogResult> callback, Type dialogWindowType);
    }
}