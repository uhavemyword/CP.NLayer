using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Common
{
    /// <summary>
    /// Interaction logic for CrudBar.xaml
    /// </summary>
    public partial class CrudBar : UserControl
    {
        public CrudBar()
        {
            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }

            InitializeComponent();
        }
    }
}