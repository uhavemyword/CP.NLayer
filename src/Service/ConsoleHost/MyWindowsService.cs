namespace CP.NLayer.Service.ConsoleHost
{
    using System.Reflection;
    using System.ServiceProcess;

    partial class MyWindowsService : ServiceBase
    {
        public MyWindowsService()
        {
            InitializeComponent();
            var titleAttribute = (AssemblyTitleAttribute)Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false)[0];
            this.ServiceName = titleAttribute.Title;
        }

        protected override void OnStart(string[] args)
        {
            ServiceHosts.Open();
        }

        protected override void OnStop()
        {
            ServiceHosts.Close();
        }
    }
}