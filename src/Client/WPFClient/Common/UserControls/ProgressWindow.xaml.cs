using System.Windows;
using Telerik.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Common
{
    /// <summary>
    /// Interaction logic for ProgressView.xaml
    /// </summary>
    public partial class ProgressWindow : RadWindow
    {
        public ProgressWindow(ProgressViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void RadButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}