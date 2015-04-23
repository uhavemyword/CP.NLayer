using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using System;
using System.Windows;
using System.Windows.Input;

namespace CP.NLayer.Client.WpfClient.Main
{
    /// <summary>
    /// Interaction logic for Shell.xaml
    /// </summary>
    public partial class Shell : Window
    {
        public Shell(IRegionManager regionManager, IModuleManager moduleManager, ShellViewModel viewModel)
        {
            InitializeComponent();
            moduleManager.ModuleDownloadProgressChanged += this.ModuleManager_ModuleDownloadProgressChanged;
            moduleManager.LoadModuleCompleted += this.ModuleManager_LoadModuleCompleted;
            this.DataContext = viewModel;
        }

        private void ModuleManager_ModuleDownloadProgressChanged(object sender, ModuleDownloadProgressChangedEventArgs e)
        {
        }

        private void ModuleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
        }

        private void PopupControl_Loaded(object sender, RoutedEventArgs e)
        {
            Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded,
                new Action(() =>
                {
                    MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                })
            );
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var viewModel = (ShellViewModel)this.DataContext;
            viewModel.InitializeAndCheckDatabaseAsync();
        }

        //private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    var response = MessageBox.Show("Do you really want to exit?", "Exiting...",
        //        MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
        //    if (response == MessageBoxResult.No)
        //    {
        //        e.Cancel = true;
        //    }
        //    else
        //    {
        //        Application.Current.Shutdown();
        //    }
        //}
    }
}