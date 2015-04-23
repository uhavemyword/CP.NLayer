using CP.NLayer.Resources.UI;
using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Modules.Misc.About
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            viewModel.HeaderText = UiResources.About;
            this.DataContext = viewModel;
        }
    }
}