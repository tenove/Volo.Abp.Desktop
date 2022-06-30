using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Desktop.UI.Core.Threading;
using Volo.Abp.Guids;
using Volo.Abp.ObjectMapping;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Uow;

namespace Volo.Abp.Desktop.UI.ViewModels
{
    public abstract class AppViewModel : ObservableObject, ITransientDependency  
    {
        protected IAbpLazyServiceProvider LazyServiceProvider { get; }
        protected IDispatcher Dispatcher => LazyServiceProvider.LazyGetRequiredService<IDispatcher>();
        protected IGuidGenerator GuidGenerator => LazyServiceProvider.LazyGetService<IGuidGenerator>(SimpleGuidGenerator.Instance);
        protected IUnitOfWorkManager UnitOfWorkManager => LazyServiceProvider.LazyGetRequiredService<IUnitOfWorkManager>();
        protected ILoggerFactory LoggerFactory => LazyServiceProvider.LazyGetRequiredService<ILoggerFactory>();
        protected ILogger Logger => LazyServiceProvider.LazyGetService<ILogger>(provider => LoggerFactory?.CreateLogger(GetType().FullName) ?? NullLogger.Instance);
        protected Type ObjectMapperContext { get; set; }
        protected IObjectMapper ObjectMapper => LazyServiceProvider.LazyGetService<IObjectMapper>((IServiceProvider provider) => (!(ObjectMapperContext == null)) ? ((IObjectMapper)provider.GetRequiredService(typeof(IObjectMapper<>).MakeGenericType(ObjectMapperContext))) : provider.GetRequiredService<IObjectMapper>());

        protected AppViewModel(IAbpLazyServiceProvider abpLazyServiceProvider)
           : this()
        {
            LazyServiceProvider = abpLazyServiceProvider;
        }

        protected AppViewModel()
        {
        }

        #region Property

        private string title = string.Empty;

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        /// <value>The title.</value>
        public string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        private string subtitle = string.Empty;

        /// <summary>
        /// Gets or sets the subtitle.
        /// </summary>
        /// <value>The subtitle.</value>
        public string Subtitle
        {
            get => subtitle;
            set => SetProperty(ref subtitle, value);
        }

        private string icon = string.Empty;

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        /// <value>The icon.</value>
        public string Icon
        {
            get => icon;
            set => SetProperty(ref icon, value);
        }

        private bool isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get => isBusy;
            set
            {
                if (SetProperty(ref isBusy, value))
                    IsNotBusy = !isBusy;
            }
        }

        private bool isNotBusy = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance is not busy.
        /// </summary>
        /// <value><c>true</c> if this instance is not busy; otherwise, <c>false</c>.</value>
        public bool IsNotBusy
        {
            get => isNotBusy;
            set
            {
                if (SetProperty(ref isNotBusy, value))
                    IsBusy = !isNotBusy;
            }
        }

        private bool canLoadMore = true;

        /// <summary>
        /// Gets or sets a value indicating whether this instance can load more.
        /// </summary>
        /// <value><c>true</c> if this instance can load more; otherwise, <c>false</c>.</value>
        public bool CanLoadMore
        {
            get => canLoadMore;
            set => SetProperty(ref canLoadMore, value);
        }

        private string header = string.Empty;

        /// <summary>
        /// Gets or sets the header.
        /// </summary>
        /// <value>The header.</value>
        public string Header
        {
            get => header;
            set => SetProperty(ref header, value);
        }

        private string? footer = string.Empty;

        /// <summary>
        /// Gets or sets the footer.
        /// </summary>
        /// <value>The footer.</value>
        public string Footer
        {
            get => footer;
            set => SetProperty(ref footer, value);
        }

        #endregion Property

        /// <summary>
        ///
        /// </summary>
        /// <param name="navigationData"></param>
        /// <returns></returns>
        public virtual async Task InitializeAsync(object navigationData)
        {
            await Task.FromResult(false);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public object GetPropertyValue(string propertyName)
        {
            return GetType().GetProperty(propertyName).GetValue(this, null);
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public T GetPropertyValue<T>(string propertyName)
        {
            return (T)Convert.ChangeType(GetPropertyValue(propertyName), typeof(T));
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="ex"></param>
        /// <param name="shouldCatch"></param>
        /// <param name="shouldDisplay"></param>
        /// <returns></returns>
        public bool LogException(Exception ex, bool shouldCatch = false, bool shouldDisplay = false)
        {
            if (ex == null) return shouldCatch;

            Logger?.LogException(ex);
            if (shouldDisplay)
            {
                //Dispatcher.CurrentDispatcher.Invoke(() =>
                //{
                //    _ = Task.Run(() => _dialogCoordinator.ShowMessageAsync(this, _localizer?["Error"] ?? "Error", (_localizer?["Failed"] ?? "Failed") + $": {ex.ToStringDemystified()}"));
                //});
            }

            return shouldCatch;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="func"></param>
        /// <param name="loadingMessage"></param>
        /// <param name="showException"></param>
        /// <returns></returns>
        public async Task SetBusyAsync(Func<Task> func, string loadingMessage = null, bool showException = true)
        {
            IsBusy = true;
            try
            {
                await func();
            }
            catch (Exception ex) when (LogException(ex, true, showException))
            {
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}