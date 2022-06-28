using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Desktop.UI.Dialogs;
using CommunityToolkit.Mvvm.Input;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public abstract class AppDialogViewModel : AppViewModel, IDialogAware
    {
        public event Action<IDialogResult> RequestClose;

        public IRelayCommand SaveCommand { get; private set; }
        public IRelayCommand CancelCommand { get; private set; }

        protected AppDialogViewModel(IAbpLazyServiceProvider abpLazyServiceProvider) : base(abpLazyServiceProvider)
        {
            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(Cancel);
        }

        protected AppDialogViewModel()
        {
        }

        /// <summary>
        ///
        /// </summary>
        public virtual void Cancel() => OnDialogClosed(ButtonResult.Cancel);

        /// <summary>
        ///
        /// </summary>
        public virtual void Save() => OnDialogClosed(ButtonResult.OK);

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public virtual bool CanCloseDialog() => true;

        /// <summary>
        ///
        /// </summary>
        /// <param name="result"></param>
        public void OnDialogClosed(ButtonResult result)
        {
            RequestClose?.Invoke(new DialogResult(result));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="dialogResult"></param>
        public void OnDialogClosed(IDialogResult dialogResult)
        {
            RequestClose?.Invoke(dialogResult);
        }

        /// <summary>
        ///
        /// </summary>
        public void OnDialogClosed() => OnDialogClosed(ButtonResult.OK);

        /// <summary>
        ///
        /// </summary>
        /// <param name="parameters"></param>
        public virtual void OnDialogOpened(IDialogParameters parameters)
        {
        }
    }
}