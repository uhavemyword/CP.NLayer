namespace CP.NLayer.Client.WpfClient.Common
{
    using CP.NLayer.Client.WpfClient.Common.Views;
    using CP.NLayer.Resources.UI;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;
    using System;
    using System.Windows.Input;

    public class WizardPageViewModelBase : ViewModelBase, IRegionMemberLifetime, IConfirmNavigationRequest, IDialog, IWizardAware
    {
        protected IRegionNavigationService _navigationService;

        protected IInteractionService _interaction
        {
            get { return Container.Resolve<IInteractionService>(); }
        }

        public WizardPageViewModelBase()
        {
            this.BackCommand = new DelegateCommand(ExecuteBackCommand, CanExecuteBackCommand);
            this.NextCommand = new DelegateCommand(ExecuteNextCommand, CanExecuteNextCommand);
            this.FinishCommand = new DelegateCommand(ExecuteFinishCommand, CanExecuteFinishCommand);
            this.CancelCommand = new DelegateCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }

        #region IWizardAware

        public ICommand BackCommand { get; set; }
        public ICommand NextCommand { get; set; }
        public ICommand FinishCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public virtual void ExecuteBackCommand()
        {
            if (this._navigationService.Journal.CanGoBack)
            {
                this._navigationService.Journal.GoBack();
            }
        }

        public virtual bool CanExecuteBackCommand()
        {
            return true;
        }

        public virtual void ExecuteNextCommand()
        {
            if (this._navigationService.Journal.CanGoForward)
            {
                this._navigationService.Journal.GoForward();
            }
        }

        public virtual bool CanExecuteNextCommand()
        {
            return true;
        }

        public virtual void ExecuteFinishCommand()
        {
        }

        public virtual bool CanExecuteFinishCommand()
        {
            return false;
        }

        public virtual void ExecuteCancelCommand()
        {
            GlobalCommands.NavigateToSingleActiveRegion(RegionNames.MainContentRegion, typeof(MainBlankView));
        }

        public virtual bool CanExecuteCancelCommand()
        {
            return true;
        }

        #endregion

        #region IRegionMemberLifetime

        public virtual bool KeepAlive
        {
            get { return false; }
        }

        #endregion IRegionMemberLifetime

        #region IConfirmNavigationRequest

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            this._navigationService = navigationContext.NavigationService;
        }

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            string temp = navigationContext.NavigationService.Journal.CurrentEntry.Uri.ToString();
            var currentUriParent = temp.Substring(0, temp.LastIndexOf('.'));
            temp = navigationContext.Uri.ToString();
            var toUriParent = temp.Substring(0, temp.LastIndexOf('.'));

            if (currentUriParent == toUriParent)
            {
                //assume views of a same wizard are under same namespace.

                continuationCallback(true);
            }
            else
            {
                _interaction.ShowConfirmation(UiResources.ExitWizardPrompt,
                    confirmed =>
                    {
                        continuationCallback(confirmed);
                    });
            }
        }

        #endregion INavigationAware

        #region IDialog

        public bool IsDialog { get; set; }

        private bool? _dialogResult;
        public bool? DialogResult
        {
            get { return _dialogResult; }
            set
            {
                if (_dialogResult != value)
                {
                    _dialogResult = value;
                    this.OnPropertyChanged(() => this.DialogResult);
                }
            }
        }

        private string _headerText;
        public virtual string HeaderText
        {
            get { return _headerText; }
            set
            {
                _headerText = value;
                this.OnPropertyChanged(() => this.HeaderText);
            }
        }

        public virtual void OnShow(object arg)
        {
        }

        public virtual void OnClosing()
        {
            this.DialogResult = false;
        }

        #endregion
    }
}