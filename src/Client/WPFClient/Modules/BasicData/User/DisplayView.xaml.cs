namespace CP.NLayer.Client.WpfClient.Modules.BasicData.User
{
    using CP.NLayer.Resources.UI;
    using System.Windows.Controls;

    public partial class DisplayView : UserControl
    {
        public DisplayView(DisplayViewModel viewModel)
        {
            InitializeComponent();
            viewModel.HeaderText = UiResources.Users;
            viewModel.EditViewType = typeof(EditView);
            this.DataContext = viewModel;
        }
    }
}