﻿using CP.NLayer.Resources.UI;
using System.Windows.Controls;
using Telerik.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Modules.BasicData.User
{
    /// <summary>
    /// Interaction logic for EditView.xaml
    /// </summary>
    public partial class EditView : UserControl
    {
        public EditView(EditViewModel viewModel)
        {
            InitializeComponent();
            viewModel.ModelName = UiResources.Users;
            this.DataContext = viewModel;

            this.selectAllCheckBox.Checked += new System.Windows.RoutedEventHandler(CheckBox_CheckedChanged);
            this.selectAllCheckBox.Unchecked += new System.Windows.RoutedEventHandler(CheckBox_CheckedChanged);
        }

        private void CheckBox_CheckedChanged(object sender, System.Windows.RoutedEventArgs e)
        {
            var b = this.selectAllCheckBox.IsChecked;
            for (int i = 0; i < this.radListBox.Items.Count; i++)
            {
                var item = this.radListBox.ItemContainerGenerator.ContainerFromIndex(i) as RadListBoxItem;
                item.IsSelected = b.HasValue ? b.Value : false;
            }
        }
    }
}