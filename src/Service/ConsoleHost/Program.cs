// ------------------------------------------------------------------------------------
//      Copyright (c) 2012 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 12/24/2012 11:45:41 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.ConsoleHost
{
    using log4net;
    using System;
    using System.ServiceProcess;

    public class Program
    {
        private static readonly ILog _log = log4net.LogManager.GetLogger("System");

        public static void Main(string[] args)
        {
            if (!System.Environment.UserInteractive)
            {
                // run as windows service
                _log.Info("Starting Windows Service...");
                ServiceBase[] servicesToRun = new ServiceBase[] { new MyWindowsService() };
                ServiceBase.Run(servicesToRun);
            }
            else
            {
                // run as console app
                _log.Info("Starting ConsoleHost......");
                ServiceHosts.Open();
                _log.Info("Press 'Q' to quit.");
                while (Console.Read() != (int)'Q')
                {
                    System.Threading.Thread.Sleep(1000);
                }
                ServiceHosts.Close();
            }
        }
    }
}