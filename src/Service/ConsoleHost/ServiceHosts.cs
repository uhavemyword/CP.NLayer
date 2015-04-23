// ------------------------------------------------------------------------------------
//      Copyright (c) 2013 uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 01/04/2013 12:36:40
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Service.ConsoleHost
{
    using log4net;
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.ServiceModel;
    using System.ServiceModel.Configuration;

    public class ServiceHosts
    {
        private static IList<ServiceHost> _openedHosts = new List<ServiceHost>();
        private static readonly ILog _log = log4net.LogManager.GetLogger("System");

        public static void Open()
        {
            var serviceTypes = GetServiceTypes();
            if (serviceTypes == null || serviceTypes.Count() == 0)
            {
                throw new Exception("No service found in configuration file!");
            }

            _log.Info("=====Starting services......");
            foreach (var serviceType in serviceTypes)
            {
                var host = new ServiceHost(serviceType);
                host.Opened += delegate { _log.Info(string.Format("Service {0} is ready！", serviceType.FullName)); };
                host.Open();
                _openedHosts.Add(host);
            }
            _log.Info("=====Services started.");
        }

        public static void Close()
        {
            _log.Info("Stopping services....");
            foreach (var host in _openedHosts)
            {
                try
                {
                    host.Close();
                }
                catch (Exception)
                {
                    host.Abort();
                }
            }
            _log.Info("Services stopped.");
        }

        public static IList<Type> GetServiceTypes()
        {
            string[] files = Directory.GetFiles(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "CP.NLayer*.dll");
            var assemblies = new List<Assembly>();
            foreach (var file in files)
            {
                var assembly = Assembly.LoadFrom(file);
                assemblies.Add(assembly);
            }

            var types = assemblies.SelectMany(x => x.GetTypes()).ToList();
            var config = ConfigurationManager.OpenExeConfiguration(Assembly.GetEntryAssembly().Location);
            var serviceModelSection = (ServiceModelSectionGroup)config.GetSectionGroup("system.serviceModel");
            var serviceTypes = new List<Type>();
            foreach (ServiceElement el in serviceModelSection.Services.Services)
            {
                var serviceType = types.Where(x => x.FullName == el.Name).FirstOrDefault();
                if (serviceType == null)
                {
                    throw new Exception(string.Format("Invalid service name [{0}] in configuration file. Please check the name or project references", el.Name));
                }
                serviceTypes.Add(serviceType);
            }

            return serviceTypes;
        }
    }
}