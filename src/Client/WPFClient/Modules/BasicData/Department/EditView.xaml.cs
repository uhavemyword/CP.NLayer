using CP.NLayer.Client.WpfClient.Common;
using CP.NLayer.Models.Business;
using CP.NLayer.Resources.UI;
using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Modules.BasicData.Department
{
    /// <summary>
    /// Interaction logic for EditView.xaml
    /// </summary>
    public partial class EditView : UserControl
    {
        public EditView(EditViewModel<DepartmentEditModel> viewModel)
        {
            InitializeComponent();
            viewModel.ModelName = UiResources.Department;
            this.DataContext = viewModel;
        }
    }
}