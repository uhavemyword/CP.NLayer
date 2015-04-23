// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 5/23/2014 12:48:43 AM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.ServiceLocation;
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;
    using Telerik.Windows.Controls;

    public static class MyAttachedProperties
    {
        #region PopupDialogResult

        public static bool? GetPopupDialogResult(DependencyObject obj)
        {
            return (bool?)obj.GetValue(PopupDialogResultProperty);
        }

        public static void SetPopupDialogResult(DependencyObject obj, bool? value)
        {
            obj.SetValue(PopupDialogResultProperty, value);
        }

        // Using a DependencyProperty as the backing store for PopupDialogResult.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PopupDialogResultProperty =
            DependencyProperty.RegisterAttached("PopupDialogResult", typeof(bool?), typeof(MyAttachedProperties), new PropertyMetadata(null, PopupDialogResultChanged));

        private static void PopupDialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = VisualTreeHelper.GetParent(d);
            var regionMangager = ServiceLocator.Current.GetInstance<IRegionManager>();
            object currentActiveView = regionMangager.Regions[RegionNames.MainPopupRegion].Views.FirstOrDefault(x => x.GetType().FullName == view.GetType().FullName);
            if (currentActiveView != null)
            {
                try
                {
                    regionMangager.Regions[RegionNames.MainPopupRegion].Remove(currentActiveView);
                }
                catch (ArgumentException)
                {
                    //If the view or viewmodel has value of RegionMemberLifetime.KeepAlive = false
                    //then will hit exception here, because MainPopupRegion is a AllActiveRegion
                    //and seems the Prism do something special when remove such a view from such a region.
                    //Don't konw whether this catch will introduce memory leak.
                    //Do nothing;
                }
            }
        }

        #endregion

        //http://blog.excastle.com/2010/07/25/mvvm-and-dialogresult-with-no-code-behind/

        #region WindowDialogResult

        public static readonly DependencyProperty WindowDialogResultProperty =
           DependencyProperty.RegisterAttached("WindowDialogResult", typeof(bool?), typeof(MyAttachedProperties), new PropertyMetadata(WindowDialogResultChanged));

        public static bool GetWindowDialogResult(DependencyObject obj)
        {
            return (bool)obj.GetValue(WindowDialogResultProperty);
        }

        public static void SetWindowDialogResult(Window target, bool? value)
        {
            target.SetValue(WindowDialogResultProperty, value);
        }

        private static void WindowDialogResultChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var window = d as Window;
            if (window != null)
            {
                window.DialogResult = e.NewValue as bool?;
                return;
            }

            var radWindow = d as RadWindow;
            if (radWindow != null)
            {
                radWindow.DialogResult = e.NewValue as bool?;
                if (radWindow.DialogResult.HasValue)
                {
                    radWindow.Close();
                    var owner = radWindow.Owner as Window;
                    if (owner != null)
                    {
                        owner.Activate();
                    }
                }
            }
        }

        #endregion

        #region TargetObject

        public static object GetTargetObject(DependencyObject obj)
        {
            return (object)obj.GetValue(TargetObjectProperty);
        }

        public static void SetTargetObject(DependencyObject obj, object value)
        {
            obj.SetValue(TargetObjectProperty, value);
        }

        // Using a DependencyProperty as the backing store for TargetObject.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetObjectProperty =
            DependencyProperty.RegisterAttached("TargetObject", typeof(object), typeof(MyAttachedProperties), new PropertyMetadata(null));

        #endregion
    }
}