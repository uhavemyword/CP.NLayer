using CP.NLayer.Client.WpfClient.Common;
using CP.NLayer.Resources.UI;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.UserMap
{
    public partial class View : UserControl
    {
        private List<ShapeData> _shapeDataList;

        public View()
        {
            InitializeComponent();

            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }

            this._shapeDataList = MapUtilities.LoadUserShapeData();
            SwitchToDisplayButton_Click(null, null);
        }

        private void SwitchToLayoutButton_Click(object sender, RoutedEventArgs e)
        {
            this._displayToolbar.Visibility = System.Windows.Visibility.Collapsed;
            this._layoutToolbar.Visibility = System.Windows.Visibility.Visible;
            this._mapContainer.Content = new LayoutView(this._shapeDataList);
        }

        private void SwitchToDisplayButton_Click(object sender, RoutedEventArgs e)
        {
            var layout = this._mapContainer.Content as LayoutView;
            if (layout != null && layout.IsLayoutChanged == true)
            {
                ServiceLocator.Current.GetInstance<IInteractionService>().ShowConfirmation(UiResources.LayoutChangedPrompt, confirmed =>
                {
                    if (confirmed)
                    {
                        SaveButton_Click(null, null);
                    }
                });
            }

            this._layoutToolbar.Visibility = System.Windows.Visibility.Collapsed;
            this._displayToolbar.Visibility = System.Windows.Visibility.Visible;
            this._mapContainer.Content = new DisplayView(this._shapeDataList);
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            var layout = this._mapContainer.Content as LayoutView;
            if (layout != null)
            {
                layout.SaveShapeDataList();

                //save to DB
                MapUtilities.SaveUserShapeData(layout.ShapeDataList);
                this._shapeDataList = MapUtilities.LoadUserShapeData();
                layout.ShapeDataList = this._shapeDataList;
                layout.LoadMap();
            }
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            var layout = this._mapContainer.Content as LayoutView;
            if (layout != null)
            {
                layout.ShapeDataList = this._shapeDataList;
                layout.LoadMap();
            }
        }
    }
}