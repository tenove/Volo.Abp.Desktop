using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Volo.Abp.Desktop.UI.Dialogs;
using Volo.Abp.Desktop.UI.ViewModels;
using Wpf.Ui.Appearance;
using Wpf.Ui.Controls;

namespace Volo.Abp.Desktop.UI.Views
{
    /// <summary>
    /// Interaction logic for SplashWindow.xaml
    /// </summary>
    public partial class SplashWindow : UiWindow, IAppView, IDialogWindow
    {
        public SplashWindow(SplashWindowViewModel viewModel)
        {
            Theme.Apply(
              ThemeType.Dark,     // Theme type
              BackgroundType.Mica, // Background type
              true                                  // Whether to change accents automatically
            );

            DataContext = viewModel;
            InitializeComponent();
        }

        public IDialogResult Result { get; set; }
    }
}