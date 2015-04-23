// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 5/11/2014 1:37:04 AM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.ServiceLocation;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Note: The look is defined in the style.
    /// </summary>
    public class PopupControl : UserControl
    {
        public PopupControl()
        {
        }

        public string Title
        {
            get { return (string)GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty TitleProperty =
            DependencyProperty.Register("Title", typeof(string), typeof(PopupControl), new PropertyMetadata(null));

        public bool? PopupResult
        {
            get { return (bool)GetValue(PopupResultProperty); }
            set { SetValue(PopupResultProperty, value); }
        }

        public static readonly DependencyProperty PopupResultProperty =
            DependencyProperty.Register("PopupResult", typeof(bool?), typeof(PopupControl), new PropertyMetadata(null, OnPopupResultChanged));

        private static void OnPopupResultChanged(DependencyObject element, DependencyPropertyChangedEventArgs args)
        {
            var regionMangager = ServiceLocator.Current.GetInstance<IRegionManager>();
            object currentActiveView = regionMangager.Regions[RegionNames.MainPopupRegion].ActiveViews.FirstOrDefault();
            if (currentActiveView != null)
            {
                regionMangager.Regions[RegionNames.MainPopupRegion].Deactivate(currentActiveView);
            }
        }
    }
}