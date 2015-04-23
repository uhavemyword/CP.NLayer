using CP.NLayer.Resources.UI;
using System.Windows;
using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Modules.MainHeader.Login
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View(ViewModel viewModel)
        {
            viewModel.HeaderText = UiResources.Login;
            this.DataContext = viewModel;
            InitializeComponent();
#if DEBUG
            this.textBox1.Text = "User1";
            this.passwordBox1.Password = "a";
#endif
        }

        private void PasswordBox1_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Password is not a DependencyProperty, cannot set 'Binding' to it, therefore use following approach.
            var loginViewModel = this.DataContext as ViewModel;
            if (loginViewModel != null && loginViewModel.LoginModel != null)
            {
                loginViewModel.LoginModel.Password = this.passwordBox1.Password;
            }
        }
    }
}