using System.Windows.Input;

namespace CP.NLayer.Client.WpfClient.Common
{
    public interface IWizardAware
    {
        ICommand BackCommand { get; set; }
        ICommand NextCommand { get; set; }
        ICommand FinishCommand { get; set; }
        ICommand CancelCommand { get; set; }
    }
}