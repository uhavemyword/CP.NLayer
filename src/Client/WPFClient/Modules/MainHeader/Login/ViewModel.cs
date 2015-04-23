using CP.NLayer.Client.WpfClient.Common;
using CP.NLayer.Models.Business;
using CP.NLayer.Models.Entities;
using CP.NLayer.Resources.UI;
using CP.NLayer.Service.Contracts;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Windows.Input;

namespace CP.NLayer.Client.WpfClient.Modules.MainHeader.Login
{
    public class ViewModel : ViewModelBase, IDialog, INavigationAware
    {
        private CultureInfo _selectedCulture;

        public ViewModel()
        {
            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }

            this.SupportedCultures = new ObservableCollection<CultureInfo>()
            {
                new CultureInfo("zh"),
                new CultureInfo("en")
            };
            this.SelectedCulture = System.Threading.Thread.CurrentThread.CurrentUICulture;
            this.LoginModel = new LoginModel();
            this.BusyModel = new RadBusyModel();
            this.LoginCommand = new DelegateCommand(ExecuteLoginCommand);
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
        }

        #endregion INavigationAware

        public LoginModel LoginModel { get; set; }

        public CultureInfo SelectedCulture
        {
            get
            {
                return _selectedCulture;
            }
            set
            {
                _selectedCulture = value;
                GlobalCommands.RefreshLanguage(value);
                this.HeaderText = UiResources.Login;
                OnPropertyChanged(() => this.SelectedCulture);
            }
        }

        public ObservableCollection<CultureInfo> SupportedCultures { get; private set; }

        public ICommand LoginCommand { get; private set; }

        private void ExecuteLoginCommand()
        {
            User user = null;
            this.BusyModel.DoWorkAsync(
                () =>
                {
                    this.BusyModel.BusyContent = UiResources.OnLoginPrompt;
                    var userService = this.Container.Resolve<IUserService>();
                    user = userService.Login(LoginModel.UserName, LoginModel.Password);
                },
                () =>
                {
                    if (user != null)
                    {
                        GlobalObjects.CurrentUser = user;
                        this.DialogResult = true;
                    }
                    else
                    {
                        this.ValidationResults.Clear();
                        this.ValidationResults.Add(new ValidationResult(UiResources.LoginFailed));
                    }
                });
        }
    }
}