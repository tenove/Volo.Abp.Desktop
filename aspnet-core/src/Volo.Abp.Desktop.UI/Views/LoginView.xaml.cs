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
using Volo.Abp.Desktop.UI.ViewModels;
using Wpf.Ui.Controls;

namespace Volo.Abp.Desktop.UI.Views
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : UiWindow, IAppView
    {
        public LoginView(LoginViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();

            //HeaderBorder.MouseMove += (s, e) =>
            //{
            //    if (e.LeftButton == MouseButtonState.Pressed)
            //        this.DragMove();
            //};
            //btnClose.Click += (s, e) => { Environment.Exit(0); };
        }
    }
}