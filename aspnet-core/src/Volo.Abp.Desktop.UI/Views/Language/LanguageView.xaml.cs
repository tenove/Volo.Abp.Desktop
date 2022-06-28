﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Volo.Abp.Desktop.UI.ViewModels;
using Wpf.Ui.Controls;

namespace Volo.Abp.Desktop.UI.Views
{
    /// <summary>
    /// Interaction logic for LanguageView.xaml
    /// </summary>
    public partial class LanguageView : UiPage, IAppView
    {
        public LanguageView(LanguageViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}