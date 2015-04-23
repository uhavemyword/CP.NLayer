namespace CP.NLayer.Client.WpfClient.Common
{
    using CP.NLayer.Common;
    using CP.NLayer.Common.License;
    using Microsoft.Win32;
    using System;
    using System.Windows;

    /// <summary>
    /// Interaction logic for RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {
        private DateTime _expireDate;

        public RegistrationWindow()
            : this(DateTime.MinValue)
        {
        }

        public RegistrationWindow(DateTime expireDate)
        {
            InitializeComponent();
            this._expireDate = expireDate;
            if (this._expireDate < DateTime.Now)
            {
                this.TrialButton.Visibility = System.Windows.Visibility.Collapsed;
                this.CancelButton.Visibility = System.Windows.Visibility.Visible;
                this.TitleTextBlock.Text = "Application has expired.";
            }
            else
            {
                this.TrialButton.Visibility = System.Windows.Visibility.Visible;
                this.CancelButton.Visibility = System.Windows.Visibility.Collapsed;
                this.TitleTextBlock.Text = string.Format("License will expire in {0} day(s).", (expireDate - DateTime.Now.Date).Days);
            }
            this.SerialNumberTextBox.Text = MachineKey.Create().Key;
        }

        private void RegisterButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.RegistrationCodeTextBox.Text.Trim() != string.Empty)
            {
                bool result = false;
                var productKey = new ProductKey(this.RegistrationCodeTextBox.Text.Trim());
                if (productKey.IsValid && productKey.ExpireDate > DateTime.Now
                    && (productKey.Version.Applicagtion == ApplicationEnum.Unspecified ||
                                (int)productKey.Version.Applicagtion == Utility.GetMajorVersion())
                    && productKey.MachineKey.Key == this.SerialNumberTextBox.Text)
                {
                    RegistryKey rk = Registry.CurrentUser.CreateSubKey("SOFTWARE\\CP_NLayer");
                    string keyName = "pk"; //product key
                    rk.SetValue(keyName, productKey.Key);
                    GlobalObjects.ProductKey = productKey;
                    result = true;
                }
                else
                {
                    MessageBox.Show("The registration code is invalid or expired!", "Application", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                this.DialogResult = result;
            }
        }

        private void TrialButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}