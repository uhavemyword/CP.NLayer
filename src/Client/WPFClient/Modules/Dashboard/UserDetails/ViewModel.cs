using CP.NLayer.Client.WpfClient.Common;
using CP.NLayer.Models.Entities;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using System.Collections.Generic;
using System.Linq;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.UserDetails
{
    public class ViewModel : ViewModelBase
    {
        public ViewModel()
        {
            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }

            SubscribeSelectedUsersChangedEvent();
        }

        private Model _item;
        public Model Item
        {
            get { return _item; }
            set
            {
                if (!object.Equals(_item, value))
                {
                    _item = value;
                    this.OnPropertyChanged(() => this.Item);
                }
            }
        }

        #region SubscribeEvent

        private SubscriptionToken _subscriptionToken;

        private void SubscribeSelectedUsersChangedEvent()
        {
            var selectedUsersChangedEvent = ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<SelectedUsersChangedEvent>();
            if (_subscriptionToken != null)
            {
                selectedUsersChangedEvent.Unsubscribe(_subscriptionToken);
            }
            _subscriptionToken = selectedUsersChangedEvent.Subscribe(SelectedUsersChangedEventHandler);
        }

        private void SelectedUsersChangedEventHandler(List<User> users)
        {
            var user = users.FirstOrDefault();
            this.Item = user == null ? null : new Model(user);
        }

        #endregion
    }
}