using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Volo.Abp.Desktop.UI.Core.Threading;
using Volo.Abp.Desktop.UI.ViewModels;
using Wpf.Ui.Appearance;
using Wpf.Ui.Common;
using Wpf.Ui.Controls;
using Wpf.Ui.Controls.Interfaces;
using Wpf.Ui.Mvvm.Contracts;
using Wpf.Ui.TaskBar;

namespace Volo.Abp.Desktop.UI.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : UiWindow, IAppView
    {
        private bool _initialized = false;
        private readonly IThemeService _themeService;
        private readonly ITaskBarService _taskBarService;

        /// <summary>
        ///
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="navigationService"></param>
        /// <param name="pageService"></param>
        /// <param name="themeService"></param>
        /// <param name="taskBarService"></param>
        public MainWindow(MainWindowViewModel viewModel, INavigationService navigationService, IPageService pageService, IThemeService themeService, ITaskBarService taskBarService)
        {
            _themeService = themeService;
            _taskBarService = taskBarService;

            DataContext = viewModel;
            InitializeComponent();

            SetPageService(pageService);

            navigationService.SetNavigation(RootNavigation);

            Loaded += (_, _) => InvokeSplashScreen();
        }

        /// <summary>
        /// Raises the closed event.
        /// </summary>
        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            System.Windows.Application.Current.Shutdown();
        }

        #region INavigationWindow methods

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Frame GetFrame() => RootFrame;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public Wpf.Ui.Controls.Interfaces.INavigation GetNavigation() => RootNavigation;

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageType"></param>
        /// <returns></returns>
        public bool Navigate(Type pageType) => RootNavigation.Navigate(pageType);

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageService"></param>
        public void SetPageService(IPageService pageService) => RootNavigation.PageService = pageService;

        /// <summary>
        ///
        /// </summary>
        public void ShowWindow() => Show();

        /// <summary>
        ///
        /// </summary>
        public void CloseWindow() => Close();

        #endregion INavigationWindow methods

        /// <summary>
        ///
        /// </summary>
        private void InvokeSplashScreen()
        {
            if (_initialized)
                return;

            _initialized = true;

            _taskBarService.SetState(this, TaskBarProgressState.Indeterminate);

            Task.Run(async () =>
            {
                await Task.Delay(400);

                await Dispatcher.InvokeAsync(() =>
                {
                    Navigate(typeof(Dashboard));

                    _taskBarService.SetState(this, TaskBarProgressState.None);
                });

                return true;
            });
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigationButtonTheme_OnClick(object sender, RoutedEventArgs e)
        {
            _themeService.SetTheme(_themeService.GetTheme() == ThemeType.Dark ? ThemeType.Light : ThemeType.Dark);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TrayMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            if (sender is not System.Windows.Controls.MenuItem menuItem)
                return;

            System.Diagnostics.Debug.WriteLine($"DEBUG | WPF UI Tray clicked: {menuItem.Tag}", "Wpf.Ui.Demo");
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RootNavigation_OnNavigated(Wpf.Ui.Controls.Interfaces.INavigation sender, RoutedNavigationEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine($"DEBUG | WPF UI Navigated to: {sender?.Current ?? null}", "Wpf.Ui.Demo");

            // This funky solution allows us to impose a negative
            // margin for Frame only for the Dashboard page, thanks
            // to which the banner will cover the entire page nicely.
            RootFrame.Margin = new Thickness(
                left: 0,
                top: sender?.Current?.PageTag == "dashboard" ? -69 : 0,
                right: 0,
                bottom: 0);
        }
    }
}