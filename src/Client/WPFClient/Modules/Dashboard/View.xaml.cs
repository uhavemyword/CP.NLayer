using CP.NLayer.Client.WpfClient.Common;
using System.Windows;
using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard
{
    public partial class View : UserControl
    {
        public View()
        {
            this.DataContext = new ViewModel();
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            WpfHelper.RegisterHotKey(this.Menu);
        }
    }
}