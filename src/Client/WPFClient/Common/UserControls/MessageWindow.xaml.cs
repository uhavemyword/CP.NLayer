using System;
using System.Windows;
using System.Windows.Threading;
using Telerik.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Common
{
    /// <summary>
    /// Interaction logic for MessageWindow.xaml
    /// </summary>
    public partial class MessageWindow : RadWindow
    {
        private DispatcherTimer _timer;
        private int _closedInSeconds;

        public MessageWindow(string title, string message, int closedInSeconds)
        {
            InitializeComponent();
            this.Header = title;
            this._messageTextBlock.Text = message;
            this._closedInSeconds = closedInSeconds;
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            if (this._closedInSeconds > 0)
            {
                this._timer = new DispatcherTimer();
                this._timer.Interval = TimeSpan.FromSeconds(1);
                this._timer.Tick += Timer_Tick;
                this._timer.Start();
                Timer_Tick(this, null);
            }
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            this._closeButton.Content = string.Format("Close({0})", --_closedInSeconds);
            if (_closedInSeconds <= 0)
            {
                this._timer.Stop();
                this.Close();
            }
        }

        private void ClosedButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}