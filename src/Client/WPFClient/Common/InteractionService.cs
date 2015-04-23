// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 02/19/2013 21:51:53
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using CP.NLayer.Common;
    using CP.NLayer.Resources.UI;
    using Microsoft.Practices.Prism;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.ServiceLocation;
    using Microsoft.Practices.Unity;
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using Telerik.Reporting;
    using Telerik.ReportViewer.Wpf;
    using Telerik.Windows.Controls;

    public class InteractionService : IInteractionService
    {
        private Action<bool> _onConfirmationClosed;
        private Action<bool, string> _onPromptClosed;
        private Action _onAlertClosed;
        private Action _onErrorClosed;

        public void ShowConfirmation(string content, Action<bool> onConfirmationClosed)
        {
            this._onConfirmationClosed = onConfirmationClosed;
            RadWindow.Confirm(new DialogParameters()
            {
                Content = FormatContent(content),
                Closed = new EventHandler<WindowClosedEventArgs>(OnConfirmationClosed),
                DialogStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                Header = UiResources.Confirm,
                OkButtonContent = UiResources.Ok,
                CancelButtonContent = UiResources.Cancel,
                Owner = WpfHelper.GetActiveWindow()
            });
        }

        public void ShowPrompt(string content, string defaultPromptResult, Action<bool, string> onPrompted)
        {
            this._onPromptClosed = onPrompted;
            RadWindow.Prompt(new DialogParameters()
            {
                Content = FormatContent(content),
                Closed = OnPromptClosed,
                DialogStartupLocation = WindowStartupLocation.CenterOwner,
                Header = UiResources.Prompt,
                OkButtonContent = UiResources.Ok,
                CancelButtonContent = UiResources.Cancel,
                Owner = WpfHelper.GetActiveWindow(),
                DefaultPromptResultValue = defaultPromptResult
            });
        }

        public void ShowAlert(string content, Action onAlertClosed)
        {
            this._onAlertClosed = onAlertClosed;
            RadWindow.Alert(new DialogParameters()
            {
                Content = FormatContent(content),
                Closed = new EventHandler<WindowClosedEventArgs>(OnAlertClosed),
                DialogStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                Header = UiResources.Alert,
                OkButtonContent = UiResources.Ok,
                Owner = WpfHelper.GetActiveWindow()
            });
        }

        public void ShowError(string content, Action onErrorClosed)
        {
            this._onErrorClosed = onErrorClosed;
            RadWindow.Alert(new DialogParameters()
            {
                Content = FormatContent(content),
                Closed = new EventHandler<WindowClosedEventArgs>(OnErrorClosed),
                DialogStartupLocation = System.Windows.WindowStartupLocation.CenterOwner,
                Header = UiResources.Error,
                OkButtonContent = UiResources.Ok,
                Owner = WpfHelper.GetActiveWindow()
            });
        }

        private static ScrollViewer FormatContent(string content)
        {
            return new ScrollViewer
            {
                VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled,
                MaxWidth = 350,
                MaxHeight = 200,
                BorderThickness = new Thickness(0),
                Content = new TextBlock
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    TextWrapping = TextWrapping.Wrap,
                    Text = content
                }
            };
        }

        public void ShowException(Exception exception)
        {
            var message = exception.CustomMessage();
            if (string.IsNullOrEmpty(message))
            {
                ShowError(exception.MostInnerException().Message, null);
            }
            else
            {
                ShowError(message, null);
            }
        }

        public void ShowMessage(string title, string message, int closedInSeconds)
        {
            var window = new MessageWindow(title, message, closedInSeconds);
            window.Owner = WpfHelper.GetActiveWindow();
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            window.ShowDialog();
        }

        public void ShowView(object viewOrViewType, bool modal, object arg, Action<bool> onClosed)
        {
            FrameworkElement content = null;
            if (viewOrViewType is Type)
            {
                var container = ServiceLocator.Current.GetInstance<IUnityContainer>();
                var viewType = (Type)viewOrViewType;
                content = container.Resolve(viewType) as FrameworkElement;
            }
            else
            {
                content = viewOrViewType as FrameworkElement;
            }

            if (content != null)
            {
                var dialog = content.DataContext as IDialog;
                if (dialog != null)
                {
                    dialog.IsDialog = true;
                    dialog.OnShow(arg);

                    var window = new MyWindow();
                    window.HideMaximizeButton = true;
                    window.OnWindowClosing = dialog.OnClosing;
                    window.OnWindowClosed = onClosed;
                    window.Content = content;
                    window.DataContext = content.DataContext;
                    window.Owner = WpfHelper.GetActiveWindow();
                    window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;

                    if (modal)
                    {
                        window.ShowDialog();
                    }
                    else
                    {
                        window.Show();
                    }
                }
            }
        }

        public void ShowReportWindow(ReportSource reportSource)
        {
            var reportViewer = new ReportViewer();
            reportViewer.ReportSource = reportSource;

            var window = new RadWindow();
            window.Header = UiResources.Reports;
            window.Content = reportViewer;
            window.Owner = WpfHelper.GetActiveWindow();
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            window.SizeToContent = false;
            window.WindowState = WindowState.Maximized;
            window.Show();
        }

        public void ShowPopupView(Type viewType, UriQuery query)
        {
            query = query ?? new UriQuery();
            var regionManager = ServiceLocator.Current.GetInstance<IRegionManager>();

            var region = regionManager.Regions[RegionNames.MainPopupRegion];
            if (!region.Views.Any(x => x.GetType().FullName == viewType.FullName))
            {
                regionManager.RegisterViewWithRegion(RegionNames.MainPopupRegion, viewType);
            }
            regionManager.RequestNavigate(RegionNames.MainPopupRegion, new Uri(viewType.FullName + query.ToString(), UriKind.Relative));
        }

        public void ShowProgress(ProgressViewModel progressViewModel, RoutedEventHandler onLoaded, bool closeOnCompleted)
        {
            var window = new ProgressWindow(progressViewModel);
            window.Loaded += onLoaded;
            window.Owner = WpfHelper.GetActiveWindow();
            window.WindowStartupLocation = System.Windows.WindowStartupLocation.CenterOwner;
            window.SizeToContent = true;
            if (closeOnCompleted)
            {
                progressViewModel.Completed += (message) => window.Dispatcher.Invoke(new Action(() => window.Close()));
            }
            window.ShowDialog();
        }

        private void OnConfirmationClosed(object sender, WindowClosedEventArgs e)
        {
            if (this._onConfirmationClosed != null)
            {
                try
                {
                    this._onConfirmationClosed(e.DialogResult.HasValue ? e.DialogResult.Value : false);
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
                this._onConfirmationClosed = null;
            }
        }

        private void OnPromptClosed(object sender, WindowClosedEventArgs e)
        {
            if (this._onPromptClosed != null)
            {
                try
                {
                    this._onPromptClosed(e.DialogResult.HasValue ? e.DialogResult.Value : false, e.PromptResult);
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
                this._onPromptClosed = null;
            }
        }

        private void OnAlertClosed(object sender, WindowClosedEventArgs e)
        {
            if (this._onAlertClosed != null)
            {
                try
                {
                    this._onAlertClosed();
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
                this._onAlertClosed = null;
            }
        }

        private void OnErrorClosed(object sender, WindowClosedEventArgs e)
        {
            if (this._onErrorClosed != null)
            {
                try
                {
                    this._onErrorClosed();
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
                this._onErrorClosed = null;
            }
        }
    }
}