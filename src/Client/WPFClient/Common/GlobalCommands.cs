namespace CP.NLayer.Client.WpfClient.Common
{
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.ServiceLocation;
    using System;
    using System.Globalization;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media;
    using Telerik.Windows.Controls;

    public class GlobalCommands
    {
        public static ICommand GlobalNavigateCommand = new DelegateCommand<object>(NavigateToMainContentRegion);
        public static ICommand ShowViewCommand = new DelegateCommand<object>(ShowView);

        public static void ShowView(object viewType)
        {
            var interaction = ServiceLocator.Current.GetInstance<IInteractionService>();
            interaction.ShowView(viewType, true, null, null);
        }

        public static void NavigateToMainContentRegion(object viewType)
        {
            NavigateToSingleActiveRegion(RegionNames.MainContentRegion, (Type)viewType);
        }

        public static void NavigateToSingleActiveRegion(string regionName, Type viewType)
        {
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();
            if (regionManager == null)
            {
                throw new InvalidOperationException("regionManager is null!");
            }

            var region = regionManager.Regions[regionName] as SingleActiveRegion;
            if (region == null)
            {
                throw new InvalidOperationException("region is null or not a SingleActiveRegion!");
            }

            if (viewType != null)
            {
                //if (!region.Views.Any(x => x.GetType() == (Type)viewType))
                //{
                //    regionManager.RegisterViewWithRegion(regionName, (Type)viewType);
                //}

                //if (!region.ActiveViews.Any(x => x.GetType() == (Type)viewType))
                //{
                regionManager.RequestNavigate(regionName, viewType.FullName);
                //}
            }
        }

        public static void InitializeDatabase(bool force)
        {
            var service = ServiceLocator.Current.GetInstance<ISystemService>();
            service.InitializeDatabase(force);
        }

        public static void RefreshLanguage(CultureInfo culture)
        {
            if (culture != null)
            {
                WPFLocalizeExtension.Engine.LocalizeDictionary.Instance.Culture = culture;

                //restore CurrentCulture which will affect time, currency format
                //System.Threading.Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US")
                //{
                //    DateTimeFormat = new DateTimeFormatInfo() { ShortDatePattern = "MM/dd/yyyy" }
                //};
            }
        }

        public static void ApplyTheme(string name)
        {
            Application.Current.Resources.MergedDictionaries.Clear();

            //Generic
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Common;component/Resources.xaml")
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/Generic.xaml")
            });

            #region Specific

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/{0}/Telerik.Windows.Controls.xaml", name))
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/{0}/System.Windows.xaml", name))
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/{0}/Telerik.Windows.Controls.Input.xaml", name))
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/{0}/Telerik.Windows.Controls.Navigation.xaml", name))
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/{0}/Telerik.Windows.Controls.Data.xaml", name))
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/{0}/Telerik.Windows.Controls.Docking.xaml", name))
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/{0}/Telerik.Windows.Controls.GridView.xaml", name))
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/{0}/Telerik.Windows.Documents.xaml", name))
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/{0}/Telerik.ReportViewer.Wpf.xaml", name))
            });

            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/{0}/ImageResources.xaml", name))
            });
            Application.Current.Resources.MergedDictionaries.Add(new ResourceDictionary()
            {
                Source = new Uri(string.Format(@"pack://application:,,,/CP.NLayer.Client.WpfClient.Resources;component/Themes/{0}/CustomStyles.xaml", name))
            });

            #endregion

            //Change Telerik Windows8 default color
            Windows8Palette.Palette.AccentColor = (Color)ColorConverter.ConvertFromString("#FF5CACEE");
        }
    }
}