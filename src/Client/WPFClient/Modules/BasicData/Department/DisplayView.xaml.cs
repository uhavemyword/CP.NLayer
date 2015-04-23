namespace CP.NLayer.Client.WpfClient.Modules.BasicData.Department
{
    using CP.NLayer.Client.WpfClient.Common;
    using CP.NLayer.Models.Business;
    using CP.NLayer.Resources.UI;
    using System.Windows.Controls;

    public partial class DisplayView : UserControl
    {
        public DisplayView(DisplayViewModel<DepartmentDisplayModel> viewModel)
        {
            InitializeComponent();
            viewModel.HeaderText = UiResources.Departments;
            viewModel.EditViewType = typeof(EditView);
            this.DataContext = viewModel;
        }
    }
}