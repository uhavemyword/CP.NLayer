namespace CP.NLayer.Client.WpfClient.Main
{
    using CP.NLayer.Client.WpfClient.Common;
    using CP.NLayer.Common;
    using CP.NLayer.Common.License;
    using Microsoft.Practices.Unity;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Threading;

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    [LicenseProvider(typeof(MyLicenseProvider))]
    public partial class App : Application
    {
        public static bool CheckLicese()
        {
            object license = LicenseManager.Validate(typeof(App), App.Current);

            var myLicense = license as MyLicense;
            var productKey = myLicense.ProductKey;
            GlobalObjects.ProductKey = productKey;

            if (productKey != null
                && productKey.IsValid
                && (productKey.ExpireDate - DateTime.Now.Date).Days > 7)
            {
                return true;
            }
            else
            {
                RegistrationWindow form;
                if (productKey == null)
                {
                    form = new RegistrationWindow();
                }
                else
                {
                    form = new RegistrationWindow(productKey.ExpireDate);
                }
                var result = form.ShowDialog();
                if (result.HasValue && result.Value)
                {
                    return true;
                }
            }

            return false;
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            log4net.LogManager.GetLogger(typeof(App)).Info("Starting application...");
            App.Current.ShutdownMode = System.Windows.ShutdownMode.OnExplicitShutdown;

            if (!CheckLicese())
            {
                Application.Current.Shutdown();
                return;
            }

            try
            {
                string logPath = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "logs");
                Utility.EnsureFloderWritable(logPath);
            }
            catch
            {
                var interaction = DependencyInjection.Container.Resolve<IInteractionService>();
                interaction.ShowError("Please make sure the logs folder is writable, otherwise logging will not work.", null);
            }

            GlobalCommands.ApplyTheme("Windows8");
            GlobalCommands.RefreshLanguage(new CultureInfo("en"));

            // set exception handler
            Application.Current.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(App_DispatcherUnhandledException);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

            // set DataDirectory
            AppDomain.CurrentDomain.SetData("DataDirectory", AppDomain.CurrentDomain.BaseDirectory);

            App.Current.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;

            base.OnStartup(e);
            Bootstrapper bootstrapper = new Bootstrapper();
            bootstrapper.Run();
        }

        /// <summary>
        /// Handles the exceptions thrown in current application, the app will NOT be terminated.
        /// </summary>
        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            log4net.LogManager.GetLogger(typeof(App)).Error("App_DispatcherUnhandledException caught", e.Exception);
            var interaction = DependencyInjection.Container.Resolve<IInteractionService>();
            var message = e.Exception.CustomMessage();
            if (string.IsNullOrEmpty(message))
            {
                //comment out as user hit error on PrintScreen
                //interaction.ShowError(e.Exception.Message, () => PrintScreen());

                interaction.ShowError(e.Exception.MostInnerException().Message, null);
            }
            else
            {
                interaction.ShowError(message, null);
            }
            e.Handled = true;
        }

        /// <summary>
        /// Handles the exceptions which not handled by App_DispatcherUnhandledException, and the app will be terminated.
        /// </summary>
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = (Exception)e.ExceptionObject;
            log4net.LogManager.GetLogger(typeof(App)).Fatal("CurrentDomain_UnhandledException caught", ex);
        }

        private void PrintScreen()
        {
            var path = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "logs", "screenshots");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            var fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".jpg";

            using (Bitmap printscreen = new Bitmap(Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenWidth), Convert.ToInt32(System.Windows.SystemParameters.PrimaryScreenHeight)))
            {
                using (Graphics graphics = Graphics.FromImage(printscreen as Image))
                {
                    graphics.CopyFromScreen(0, 0, 0, 0, printscreen.Size);
                }
                printscreen.Save(Path.Combine(path, fileName), System.Drawing.Imaging.ImageFormat.Jpeg);
            }
        }
    }
}