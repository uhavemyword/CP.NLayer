// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 02/19/2013 21:51:31
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using Microsoft.Practices.Prism;
    using System;
    using System.Windows;
    using Telerik.Reporting;

    public interface IInteractionService
    {
        void ShowConfirmation(string content, Action<bool> onConfirmationClosed);

        void ShowPrompt(string content, string defaultPromptResult, Action<bool, string> onPrompted);

        void ShowAlert(string content, Action onClosed);

        void ShowError(string content, Action onClosed);

        void ShowException(Exception exception);

        void ShowMessage(string title, string message, int closedInSeconds);

        void ShowView(object viewOrViewType, bool modal, object arg, Action<bool> onClosed);

        void ShowReportWindow(ReportSource reportSource);

        void ShowProgress(ProgressViewModel progressViewModel, RoutedEventHandler onLoaded, bool closeOnCompleted);

        void ShowPopupView(Type viewType, UriQuery query);
    }
}