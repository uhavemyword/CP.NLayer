using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.Home
{
    /// <summary>
    /// Interaction logic for DashBoardView.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View()
        {
            this.DataContext = new ViewModel();
            InitializeComponent();
        }
    }
}