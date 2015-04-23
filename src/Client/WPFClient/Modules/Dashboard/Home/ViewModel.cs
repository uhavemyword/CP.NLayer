using CP.NLayer.Client.WpfClient.Common;
using CP.NLayer.Models.Entities;
using CP.NLayer.Service.Contracts;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using System.Collections.Generic;
using System.Linq;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.Home
{
    public class ViewModel : ViewModelBase, IRegionMemberLifetime
    {
        public static List<User> Users { get; private set; }

        public ViewModel()
        {
            LoadData();
        }

        public static void PublishUsersColorChangedEvent(UsersColorModel payload)
        {
            var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
            eventAggregator.GetEvent<UsersColorChangedEvent>().Publish(payload);
        }

        private void LoadData()
        {
            var userService = this.Container.Resolve<IUserService>();
            Users = userService.GetAll().ToList();
        }

        #region IRegionMemberLifetime

        public virtual bool KeepAlive
        {
            get { return false; }
        }

        #endregion IRegionMemberLifetime
    }
}