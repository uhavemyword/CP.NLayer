// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 5/28/2014 10:34:56 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using Telerik.Windows.Controls;

    public static class WpfHelper
    {
        public static bool GetIsInDesignMode()
        {
            return System.ComponentModel.DesignerProperties.GetIsInDesignMode(new DependencyObject());
        }

        public static T GetParentVisualElement<T>(DependencyObject dp) where T : DependencyObject
        {
            var parent = VisualTreeHelper.GetParent(dp);
            while (parent != null)
            {
                var t = parent as T;
                if (t != null)
                {
                    return t;
                }
                parent = VisualTreeHelper.GetParent(parent);
            }
            return null;
        }

        /// <summary>
        /// Collapse RadComboBox dropdownlist which is a RadGridView, when event PreviewMouseUp fired on RadGridView cell.
        /*
                private void RadGridView_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
                {
                    WpfHelper.RadGridViewInsideRadComboBox_PreviewMouseUp(sender, e);
                }

                <!--<telerik:RadComboBox Grid.Row="3" Grid.Column="1" IsEditable="True" MaxDropDownHeight="1000"
                	 Text="{Binding SelectedContactText, Mode=TwoWay}" Style="{StaticResource EditViewRadComboBoxStyle}" >
                    <telerik:RadComboBoxItem>
                        <telerik:RadComboBoxItem.Template>
                            <ControlTemplate>
                                <telerik:RadGridView ItemsSource="{Binding Target.AllContacts}" SelectedItem="{Binding SelectedContact, Mode=TwoWay}"
                                                     PreviewMouseUp="RadGridView_PreviewMouseUp" MaxHeight="300" ShowGroupPanel="False"
                                                     Style="{StaticResource DisplayViewDataGridStyle}" >
                                    <telerik:RadGridView.Columns>
                                        <telerik:GridViewDataColumn DataMemberBinding="{Binding Id}" />
                                        <telerik:GridViewDataColumn DataMemberBinding="{Binding CompanyName}" />
                                        <telerik:GridViewDataColumn DataMemberBinding="{Binding FirstName}" />
                                        <telerik:GridViewDataColumn DataMemberBinding="{Binding LastName}" />
                                    </telerik:RadGridView.Columns>
                                </telerik:RadGridView>
                            </ControlTemplate>
                        </telerik:RadComboBoxItem.Template>
                    </telerik:RadComboBoxItem>
                </telerik:RadComboBox>-->
         */

        /// </summary>
        public static void RadGridViewInsideRadComboBox_PreviewMouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var obj = e.OriginalSource as DependencyObject;
            while (obj != null)
            {
                var cell = obj as Telerik.Windows.Controls.GridView.GridViewCell;
                var radComboBoxItem2 = obj as Telerik.Windows.Controls.GridView.GridViewHeaderCell;
                if (cell != null)
                {
                    var grid = cell.ParentOfType<RadGridView>();
                    var gridParent = VisualTreeHelper.GetParent(grid);
                    while (gridParent != null)
                    {
                        var radComboBoxItem = gridParent as RadComboBoxItem;
                        if (radComboBoxItem != null)
                        {
                            ((RadComboBox)radComboBoxItem.Parent).IsDropDownOpen = false;
                            return;
                        }
                        gridParent = VisualTreeHelper.GetParent(gridParent);
                    }
                    return;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
        }

        public static void RegisterHotKey(RadMenu menu)
        {
            var menuItems = GetAllMenuItems(menu);
            menuItems.ForEach(x => RegisterHotKey(x));
        }

        public static void RegisterHotKey(RadMenuItem menuItem)
        {
            if (!string.IsNullOrEmpty(menuItem.InputGestureText) && menuItem.Command != null && menuItem.IsEnabled)
            {
                var helper = new KeyGestureValueSerializer();
                if (helper.CanConvertFromString(menuItem.InputGestureText, null))
                {
                    var inputGesture = helper.ConvertFromString(menuItem.InputGestureText, null) as KeyGesture;
                    if (inputGesture != null)
                    {
                        var binding = new KeyBinding(menuItem.Command, inputGesture);
                        binding.CommandParameter = menuItem.CommandParameter;
                        Application.Current.MainWindow.InputBindings.Add(binding);
                    }
                }
            }
        }

        public static List<RadMenuItem> GetMenuItemWithChildren(RadMenuItem menuItem)
        {
            var result = new List<RadMenuItem>();
            if (menuItem != null)
            {
                result.Add(menuItem);
            }

            if (menuItem.HasItems)
            {
                foreach (var i in menuItem.Items)
                {
                    var item = i as RadMenuItem;
                    if (item != null)
                    {
                        result.AddRange(GetMenuItemWithChildren(item));
                    }
                }
            }
            return result;
        }

        public static List<RadMenuItem> GetAllMenuItems(RadMenu menu)
        {
            var result = new List<RadMenuItem>();
            if (menu != null && menu.HasItems)
            {
                foreach (var i in menu.Items)
                {
                    var item = i as RadMenuItem;
                    if (item != null)
                    {
                        result.AddRange(GetMenuItemWithChildren(item));
                    }
                }
            }
            return result;
        }

        public static Window GetActiveWindow()
        {
            return Application.Current.Windows.OfType<Window>().FirstOrDefault(x => x.IsActive == true);
        }
    }
}