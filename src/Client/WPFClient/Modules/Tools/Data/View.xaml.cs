using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Modules.Tools.Data
{
    /// <summary>
    /// Interaction logic for View.xaml
    /// </summary>
    public partial class View : UserControl
    {
        public View(ViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }

        private void OpenBackupButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new System.Windows.Forms.FolderBrowserDialog();
            if (Directory.Exists(this.BackupLocationTextBox.Text))
            {
                dialog.SelectedPath = this.BackupLocationTextBox.Text;
            }

            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.BackupLocationTextBox.Text = dialog.SelectedPath;
            }
        }

        private void GenerateFileNameButton_Click(object sender, RoutedEventArgs e)
        {
            this.BackupFileNameTextBox.Text = string.Format("{0:yyyyMMddHHmmss}.bak", DateTime.Now);
        }

        private void OpenResotreButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();

            dialog.Filter = "Bak files (*.bak)|*.bak|All files (*.*)|*.*";

            if (dialog.ShowDialog() == true)
            {
                this.RestoreFilePathTextBox.Text = dialog.FileName;
            }
        }
    }
}