using CP.NLayer.Client.WpfClient.Common;
using Microsoft.Practices.Prism.Regions;

namespace CP.NLayer.Client.WpfClient.Modules.MainHeader.Popup
{
    public class ViewModel : ViewModelBase, IDialog, INavigationAware
    {
        public ViewModel()
        {
            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }
        }

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

        public void OnShow(object arg)
        {
        }

        public void OnClosing()
        {
            this.DialogResult = false;
        }

        #endregion

        #region INavigationAware

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {
        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            string arg = navigationContext.Parameters["title"];
            this.HeaderText = arg;
        }

        #endregion INavigationAware
    }
}