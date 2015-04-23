using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Telerik.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Common
{
    /// <summary>
    /// Interaction logic for MyWindow.xaml
    /// </summary>
    public partial class MyWindow : RadWindow
    {
        public MyWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Arg: DialogResult
        /// </summary>
        public Action<bool> OnWindowClosed { get; set; }

        public Action OnWindowClosing { get; set; }

        private static void SelectivelyIgnoreMouseButton(object sender, MouseButtonEventArgs e)
        {
            // Find the TextBox
            DependencyObject parent = e.OriginalSource as UIElement;
            while (parent != null && !(parent is TextBox))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            if (parent != null)
            {
                var textBox = (TextBox)parent;
                if (!textBox.IsKeyboardFocusWithin)
                {
                    // If the text box is not yet focussed, give it the focus and
                    // stop further processing of this click event.
                    textBox.Focus();
                    e.Handled = true;
                }
            }
        }

        private static void SelectAllText(object sender, RoutedEventArgs e)
        {
            var textBox = e.OriginalSource as TextBox;
            if (textBox != null)
            {
                textBox.SelectAll();
            }
        }

        private void RadWindow_Closed(object sender, WindowClosedEventArgs e)
        {
            if (this.OnWindowClosed != null)
            {
                OnWindowClosed(this.DialogResult.HasValue ? this.DialogResult.Value : false);
            }
        }

        private void RadWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //apply template - gray out window when it's deactivated.
            var hostWindow = WpfHelper.GetParentVisualElement<Window>(this);
            var template = Application.Current.FindResource("WindowTemplate") as ControlTemplate;
            hostWindow.Template = template;
            hostWindow.ApplyTemplate();

            //set focus on first control
            hostWindow.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded,
                new Action(() =>
                {
                    hostWindow.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                })
            );

            //select all text on focus
            var textBoxes = this.ChildrenOfType<TextBox>();
            foreach (var textBox in textBoxes)
            {
                textBox.AddHandler(TextBox.PreviewMouseLeftButtonDownEvent, new MouseButtonEventHandler(SelectivelyIgnoreMouseButton), true);
                textBox.AddHandler(TextBox.GotKeyboardFocusEvent, new RoutedEventHandler(SelectAllText), true);
                textBox.AddHandler(TextBox.MouseDoubleClickEvent, new RoutedEventHandler(SelectAllText), true);
            }

            //override command of close(X) button
            if (this.OnWindowClosing != null)
            {
                var closeButton = this.ChildrenOfType<RadButton>().FirstOrDefault(x => x.Name == "PART_CloseButton");
                Action<object> temp = obj => OnWindowClosing();
                closeButton.Command = new DelegateCommand(temp);
            }
        }
    }
}