using CP.NLayer.Client.WpfClient.Common;
using CP.NLayer.Common;
using CP.NLayer.Common.License;
using CP.NLayer.Resources.UI;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using System.Reflection;
using System.Windows.Input;

namespace CP.NLayer.Client.WpfClient.Modules.Misc.About
{
    //[LicenseProvider(typeof(MyLicenseProvider))]
    public class ViewModel : ViewModelBase, IRegionMemberLifetime, INavigationAware, IDialog
    {
        public ViewModel()
        {
            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }

            this.UpdateLicenseCommand = new DelegateCommand(ExecuteUpdateLicenseCommand);

            //object license = LicenseManager.Validate(typeof(ViewModel), this);
            //var myLicense = license as MyLicense;

            ApplyProductKey(GlobalObjects.ProductKey);
        }

        private string _version;
        public string Version
        {
            get { return _version; }
            set
            {
                if (!object.Equals(_version, value))
                {
                    _version = value;
                    this.OnPropertyChanged(() => this.Version);
                }
            }
        }

        private string _user;
        public string User
        {
            get { return _user; }
            set
            {
                if (!object.Equals(_user, value))
                {
                    _user = value;
                    this.OnPropertyChanged(() => this.User);
                }
            }
        }

        private string _expireDate;
        public string ExpireDate
        {
            get { return _expireDate; }
            set
            {
                if (!object.Equals(_expireDate, value))
                {
                    _expireDate = value;
                    this.OnPropertyChanged(() => this.ExpireDate);
                }
            }
        }

        public ICommand UpdateLicenseCommand { get; set; }

        public void ExecuteUpdateLicenseCommand()
        {
            var form = new RegistrationWindow(GlobalObjects.ProductKey.ExpireDate);
            var result = form.ShowDialog();
            if (result.HasValue && result.Value)
            {
                ApplyProductKey(GlobalObjects.ProductKey);
                ServiceLocator.Current.GetInstance<IInteractionService>().ShowMessage(
                    UiResources.Message, UiResources.RebootRequiredToLicenseChangeTakeEffect, 0);
            }
        }

        public void ApplyProductKey(ProductKey productKey)
        {
            this.Version = string.Format("{0}, {1}, {2}",
                                                            Assembly.GetEntryAssembly().GetName().Version.ToString(),
                                                            EnumHelper.StringValueOf(productKey.Version.Edition),
                                                            EnumHelper.StringValueOf(productKey.Version.Country));
            if (productKey.IsTrial)
            {
                this.User = "Trial";
            }
            else
            {
                this.User = productKey.MachineKey.Key;
            }
            this.ExpireDate = productKey.ExpireDate.ToShortDateString();
        }

        #region IRegionMemberLifetime

        public virtual bool KeepAlive
        {
            get { return false; }
        }

        #endregion IRegionMemberLifetime

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

        public void OnShow(object arg)
        {
        }

        public void OnClosing()
        {
            this.DialogResult = false;
        }

        #endregion
    }
}