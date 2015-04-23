namespace CP.NLayer.Service.ConsoleHost
{
    using System.ComponentModel;
    using System.Reflection;

    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
            var titleAttribute = (AssemblyTitleAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
            this.serviceInstaller1.ServiceName = titleAttribute.Title;
            this.serviceInstaller1.DisplayName = titleAttribute.Title;
            var descAttribute = (AssemblyDescriptionAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false)[0];
            this.serviceInstaller1.Description = descAttribute.Description;
            this.serviceProcessInstaller1.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.serviceInstaller1.StartType = System.ServiceProcess.ServiceStartMode.Automatic;
        }
    }
}