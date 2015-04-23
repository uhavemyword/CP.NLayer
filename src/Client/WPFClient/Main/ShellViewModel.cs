using CP.NLayer.Client.WpfClient.Common;
using Microsoft.Practices.Prism.Commands;

namespace CP.NLayer.Client.WpfClient.Main
{
    public class ShellViewModel : ViewModelBase
    {
        public ShellViewModel()
        {
            base.BusyModel = new RadBusyModel();
            NavigateCommand = new DelegateCommand<object>(GlobalCommands.NavigateToMainContentRegion);
        }

        public DelegateCommand<object> NavigateCommand { get; private set; }

        public void InitializeAndCheckDatabaseAsync()
        {
            base.BusyModel.DoWorkAsync(() => GlobalCommands.InitializeDatabase(true));
        }
    }
}