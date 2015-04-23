using System;
using System.Windows.Input;
using Telerik.Windows.Controls;

namespace CP.NLayer.Client.WpfClient.Common
{
    public class MyBusyIndicator : RadBusyIndicator
    {
        protected override void ChangeVisualState(bool useTransitions)
        {
            base.ChangeVisualState(useTransitions);

            //set focus on first control
            this.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Loaded,
                new Action(() =>
                {
                    this.MoveFocus(new TraversalRequest(FocusNavigationDirection.First));
                })
            );
        }
    }
}