using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.UserDetails
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }
    }
}