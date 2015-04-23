// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 5/24/2014 11:44:40 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    public interface IDialog
    {
        bool IsDialog { get; set; }
        bool? DialogResult { get; set; }
        string HeaderText { get; set; }

        void OnShow(object arg);

        void OnClosing();
    }
}