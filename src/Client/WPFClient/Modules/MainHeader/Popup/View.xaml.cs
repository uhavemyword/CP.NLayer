using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Modules.MainHeader.Popup
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View(ViewModel viewModel)
        {
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}