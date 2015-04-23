namespace CP.NLayer.Client.WpfClient.Common
{
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.ServiceLocation;
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;

    /// <summary>
    /// Interaction logic for PopupControl.xaml
    /// </summary>
    [Obsolete(@"User PopupControl in pure C# code instead, so as to avoid error like:
    Cannot set Name attribute value 'name' on element 'element'.
    'element' is under the scope of element 'control', which already had a name registered when it was defined in another scope.")]
    public partial class PopupControlXaml : UserControl
    {
        public PopupControlXaml()
        {
            InitializeComponent();
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