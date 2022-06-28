using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
using System.Windows;
using Volo.Abp.Desktop.UI.Dialogs;
using Volo.Abp.Desktop.UI.Core;
using Volo.Abp.Desktop.UI.WindowHost;

namespace Volo.Abp.Desktop.UI.Services
{
    /// <summary>
    /// 对话主机服务
    /// </summary>
    public class DialogHostService : DialogService, IHostDialogService
    {
        private readonly IServiceProvider _serviceProvider;

        public DialogHostService(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        public IDialogResult ShowWindow(Type control)
        {
            IDialogResult dialogResult = new DialogResult(ButtonResult.None);

            var content = _serviceProvider.GetRequiredService(control);

            if (!(content is Window dialogContent))
                throw new NullReferenceException("A dialog's content must be a Window");

            if (dialogContent is Window view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
                ViewModelLocator.SetAutoWireViewModel(view, true);

            if (!(dialogContent.DataContext is IDialogAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            if (dialogContent is IDialogWindow dialogWindow)
            {
                ConfigureDialogWindowEvents(dialogWindow, result => { dialogResult = result; });
            }

            MvvmHelpers.ViewAndViewModelAction<IDialogAware>(viewModel, d => d.OnDialogOpened(null));
            dialogContent.ShowDialog();

            return dialogResult;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="parameters"></param>
        /// <param name="IdentifierName"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>

        public async Task<IDialogResult> ShowDialogAsync(Type control, IDialogParameters parameters = null, string IdentifierName = "Root")
        {
            var dialogContent = GetDialogContent(control, IdentifierName);

            if (!(dialogContent.DataContext is IHostDialogAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogHostAware interface");

            var eventHandler = GetDialogOpenedEventHandler(viewModel, parameters);

            var dialogResult = await DialogHost.Show(dialogContent, IdentifierName, eventHandler);

            if (dialogResult == null)
                return new DialogResult(ButtonResult.Cancel);

            return (IDialogResult)dialogResult;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="name"></param>
        /// <param name="IdentifierName"></param>
        /// <returns></returns>
        /// <exception cref="NullReferenceException"></exception>
        private FrameworkElement GetDialogContent(Type control, string IdentifierName = "Root")
        {
            var content = _serviceProvider.GetRequiredService(control);
            if (!(content is FrameworkElement dialogContent))
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            if (dialogContent is FrameworkElement view && view.DataContext is null && ViewModelLocator.GetAutoWireViewModel(view) is null)
                ViewModelLocator.SetAutoWireViewModel(view, true);

            if (!(dialogContent.DataContext is IHostDialogAware viewModel))
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogHostAware interface");

            viewModel.IdentifierName = IdentifierName;

            return dialogContent;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        private DialogOpenedEventHandler GetDialogOpenedEventHandler(IHostDialogAware viewModel, IDialogParameters parameters)
        {
            if (parameters == null) parameters = new DialogParameters();

            DialogOpenedEventHandler eventHandler =
               (sender, eventArgs) =>
               {
                   var _content = eventArgs.Session.Content;
                   if (viewModel is IHostDialogAware aware)
                       aware.OnDialogOpened(parameters);
                   eventArgs.Session.UpdateContent(_content);
               };

            return eventHandler;
        }
    }
}