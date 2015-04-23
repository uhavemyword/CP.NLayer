// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/5/2013 10:02:52 AM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Modules.MainHeader
{
    using CP.NLayer.Client.WpfClient.Common;
    using Microsoft.Practices.Prism;
    using Microsoft.Practices.Prism.Commands;
    using System.Windows.Input;

    public class ViewModel : ViewModelBase
    {
        private IInteractionService _interactionService;

        public ViewModel(IInteractionService interactionService)
        {
            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }
            this._interactionService = interactionService;
            this.PopupCommand = new DelegateCommand(ExecutePopupCommand);
            this.LoginCommand = new DelegateCommand(ExecuteLoginCommand);
        }

        public ICommand PopupCommand { get; set; }

        public void ExecutePopupCommand()
        {
            var uri = new UriQuery();
            uri.Add("title", "Popup Window Header");
            _interactionService.ShowPopupView(typeof(Popup.View), uri);
        }

        public ICommand LoginCommand { get; set; }

        public void ExecuteLoginCommand()
        {
            _interactionService.ShowPopupView(typeof(Login.View), null);
        }
    }
}