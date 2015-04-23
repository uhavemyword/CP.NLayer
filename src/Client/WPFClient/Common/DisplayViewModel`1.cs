// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 2/21/2013 10:50:12 AM
// ------------------------------------------------------------------------------------

using CP.NLayer.Client.WpfClient.Common.Views;

namespace CP.NLayer.Client.WpfClient.Common
{
    using CP.NLayer.Models;
    using CP.NLayer.Resources.UI;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;

    public class DisplayViewModel<T> : ViewModelBase, IRegionMemberLifetime, INavigationAware, IDialog where T : DisplayModelBase
    {
        protected IDisplayModelService<T> _service
        {
            get { return Container.Resolve<IDisplayModelService<T>>(); }
        }

        protected IInteractionService _interaction
        {
            get { return Container.Resolve<IInteractionService>(); }
        }

        public DisplayViewModel()
        {
            base.BusyModel = new RadBusyModel();
            this.Initialize();

            this.AddCommand = new DelegateCommand(ExecuteAddCommand, CanExecuteAddCommand);
            this.EditCommand = new DelegateCommand(ExecuteEditCommand, CanExecuteEditCommand);
            this.DeleteCommand = new DelegateCommand(ExecuteDeleteCommand, CanExecuteDeleteCommand);
            this.RefreshCommand = new DelegateCommand(ExecuteRefreshCommand, CanExecuteRefreshCommand);
            this.CancelCommand = new DelegateCommand(ExecuteCancelCommand, CanExecuteCancelCommand);

            //this.SubscribeDialogResultEvent();
        }

        //#region SubscribeDialogResultEvent
        //private SubscriptionToken _subscriptionToken;
        //protected virtual void SubscribeDialogResultEvent()
        //{
        //    var dialogResultEvent = Container.Resolve<IEventAggregator>().GetEvent<DialogResultEvent>();
        //    if (_subscriptionToken != null)
        //    {
        //        dialogResultEvent.Unsubscribe(_subscriptionToken);
        //    }
        //    _subscriptionToken = dialogResultEvent.Subscribe(DialogResultEventHandler, ThreadOption.UIThread, false, DialogResultEventFilter);
        //}

        //protected virtual void DialogResultEventHandler(DialogResultEventPayload payload)
        //{
        //    if (payload.Result == true)
        //    {
        //        ExecuteRefreshCommand();
        //    }
        //}

        //protected virtual bool DialogResultEventFilter(DialogResultEventPayload payload)
        //{
        //    var editView = Container.Resolve(this.EditViewType) as FrameworkElement;
        //    if (editView != null)
        //    {
        //        bool match = payload.Subject == editView.DataContext.GetType().FullName;
        //        return match;
        //    }
        //    return false;
        //}
        //#endregion

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

        #region IRegionMemberLifetime

        public virtual bool KeepAlive
        {
            get { return false; }
        }

        #endregion IRegionMemberLifetime

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
            get
            {
                return _headerText;
            }
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

        public Type EditViewType { get; set; }

        private PagedCollectionView<T> _items;

        public PagedCollectionView<T> Items
        {
            get { return _items; }
            set
            {
                if (_items != value)
                {
                    _items = value;
                    this.OnPropertyChanged(() => this.Items);
                }
            }
        }

        private T _selectedItem;

        public T SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (!object.Equals(_selectedItem, value))
                {
                    _selectedItem = value;
                    this.OnPropertyChanged(() => this.SelectedItem);
                    this.EditCommand.RaiseCanExecuteChanged();
                    this.DeleteCommand.RaiseCanExecuteChanged();
                }
            }
        }

        private ObservableCollection<T> _selectedItems = new ObservableCollection<T>();

        public ObservableCollection<T> SelectedItems
        {
            get { return _selectedItems; }
            set
            {
                if (!object.Equals(_selectedItems, value))
                {
                    _selectedItems = value;
                    this.OnPropertyChanged(() => this.SelectedItems);
                }
            }
        }

        public DelegateCommand AddCommand { get; private set; }

        public DelegateCommand EditCommand { get; private set; }

        public DelegateCommand DeleteCommand { get; private set; }

        public DelegateCommand RefreshCommand { get; private set; }

        public DelegateCommand CancelCommand { get; private set; }

        protected virtual bool CanExecuteAddCommand()
        {
            return true;
        }

        protected virtual void ExecuteAddCommand()
        {
            _interaction.ShowView(this.EditViewType, true, null, dialogResult =>
            {
                // TODO: commented out, adding an item using ‘save and continue’, then pressing ‘cancel’, the item added does not show up in the displayView
                // if (dialogResult == true)
                //{
                ExecuteRefreshCommand();
                //}
            });
        }

        protected virtual bool CanExecuteEditCommand()
        {
            return this.SelectedItem != null;
        }

        protected virtual void ExecuteEditCommand()
        {
            if (this.SelectedItem != null)
            {
                //var query = new UriQuery();
                //query.Add("id", _service.GetId(this.SelectedItem).ToString());
                //_interaction.ShowPopupView(EditViewType, query);

                _interaction.ShowView(this.EditViewType, true, _service.GetId(this.SelectedItem), dialogResult =>
                {
                    // TODO: commented out, adding an item using ‘save and continue’, then pressing ‘cancel’, the item added does not show up in the displayView
                    //if (dialogResult == true)
                    //{
                    ExecuteRefreshCommand();
                    //}
                });
            }
        }

        protected virtual bool CanExecuteDeleteCommand()
        {
            return this.SelectedItem != null;
        }

        protected virtual bool CanExecuteCancelCommand()
        {
            return true;
        }

        protected virtual void ExecuteDeleteCommand()
        {
            if (this.SelectedItem != null)
            {
                _interaction.ShowConfirmation(
                     string.Format(UiResources.DeleteConfirm, this.SelectedItem.GetDisplayName()),
                    confirmed =>
                    {
                        if (confirmed)
                        {
                            _service.Delete(SelectedItem);
                            ExecuteRefreshCommand();
                        }
                    });
            }
        }

        protected virtual void ExecuteCancelCommand()
        {
            if (IsDialog)
            {
                this.DialogResult = false;
            }
            else
            {
                GlobalCommands.NavigateToSingleActiveRegion(RegionNames.MainContentRegion, typeof(MainBlankView));
            }
        }

        protected virtual bool CanExecuteRefreshCommand()
        {
            return true;
        }

        protected virtual void ExecuteRefreshCommand()
        {
            Initialize();
        }

        public virtual void Initialize()
        {
            this.Items = new PagedCollectionView<T>(_service.GetPage, _service.GetCount);
        }
    }
}