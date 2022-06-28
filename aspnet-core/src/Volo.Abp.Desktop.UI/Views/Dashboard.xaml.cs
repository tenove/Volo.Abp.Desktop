using System;
using System.Windows;
using Wpf.Ui.Common.Interfaces;
using Wpf.Ui.Controls;
using Wpf.Ui.Mvvm.Contracts;

namespace Volo.Abp.Desktop.UI.Views;

/// <summary>
/// Interaction logic for Dashboard.xaml
/// </summary>
[MenuType(nameof(Dashboard), typeof(Dashboard))]
public partial class Dashboard : UiPage, INavigationAware, IAppView
{
    private readonly INavigationService _navigationService;

    public Dashboard(INavigationService navigationService)
    {
        _navigationService = navigationService;

        InitializeComponent();
    }

    public void OnNavigatedTo()
    {
        System.Diagnostics.Debug.WriteLine($"INFO | {typeof(Dashboard)} navigated", "Wpf.Ui.Demo");
    }

    public void OnNavigatedFrom()
    {
        System.Diagnostics.Debug.WriteLine($"INFO | {typeof(Dashboard)} navigated out", "Wpf.Ui.Demo");
    }

    private void ButtonControls_OnClick(object sender, RoutedEventArgs e)
    {
        //  _navigationService.Navigate(typeof(Controls));
    }

    private bool TryOpenWindow(string name)
    {
        return false;
    }

    private void ButtonAction_OnClick(object sender, RoutedEventArgs e)
    {
    }

    private void ButtonExperimental_OnClick(object sender, RoutedEventArgs e)
    {
    }
}