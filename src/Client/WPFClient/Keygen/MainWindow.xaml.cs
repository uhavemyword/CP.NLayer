using CP.NLayer.Common;
using CP.NLayer.Common.License;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Keygen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var apps = EnumHelper.GetList(typeof(ApplicationEnum));
            this.ApplicationComboBox.ItemsSource = apps;
            this.ApplicationComboBox.DisplayMemberPath = "Value";
            this.ApplicationComboBox.SelectedValuePath = "Key";
            this.ApplicationComboBox.SelectedIndex = 0;

            var editions = EnumHelper.GetList(typeof(EditionEnum));
            this.EditionComboBox.ItemsSource = editions;
            this.EditionComboBox.DisplayMemberPath = "Value";
            this.EditionComboBox.SelectedValuePath = "Key";
            this.EditionComboBox.SelectedIndex = 0;

            var countries = EnumHelper.GetList(typeof(CountryEnum));
            this.CountryComboBox.ItemsSource = countries;
            this.CountryComboBox.DisplayMemberPath = "Value";
            this.CountryComboBox.SelectedValuePath = "Key";
            this.CountryComboBox.SelectedIndex = 0;

            this.ExpireDatePicker.SelectedDate = DateTime.Now.AddMonths(1);
            this.SerialCodeTextBox.Text = MachineKey.Create().Key;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.SerialCodeTextBox.Text) || this.ExpireDatePicker.SelectedDate.HasValue == false)
            {
                return;
            }

            var machineKey = new MachineKey(this.SerialCodeTextBox.Text);
            if (machineKey.IsValid == false)
            {
                MessageBox.Show("Serial code is invalid!", "Application", MessageBoxButton.OK, MessageBoxImage.Stop);
                return;
            }

            var version = new CP.NLayer.Common.License.Version((ApplicationEnum)this.ApplicationComboBox.SelectedValue,
                                                            (EditionEnum)this.EditionComboBox.SelectedValue,
                                                            (CountryEnum)this.CountryComboBox.SelectedValue);

            var productKey = ProductKey.Create(machineKey, this.ExpireDatePicker.SelectedDate.Value.Date, version, false);
            if (productKey != null && productKey.IsValid)
            {
                this.UnlockCodeTextBox.Text = productKey.Key;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Title = string.Empty;
            var productKey = new ProductKey(this.UnlockCodeTextBox.Text);
            if (productKey.IsValid)
            {
                this.Title = productKey.MachineKey.Key;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Reset();
        }

        private void SerialCodeTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            Reset();
        }

        private void ExpireDatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            this.UnlockCodeTextBox.Text = string.Empty;
        }
    }
}