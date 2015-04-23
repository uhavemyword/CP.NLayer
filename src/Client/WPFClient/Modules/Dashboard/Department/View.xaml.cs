using CP.NLayer.Models.Business.Dashboard;
using System.Linq;
using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.Department
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View()
        {
            this.DataContext = new ViewModel();
            InitializeComponent();
        }

        private void RadGridView_SelectionChanged(object sender, Telerik.Windows.Controls.SelectionChangeEventArgs e)
        {
            ((ViewModel)this.DataContext).SelectedDepartments = this._radGridView.SelectedItems.OfType<DepartmentModel>().ToList();
        }
    }
}