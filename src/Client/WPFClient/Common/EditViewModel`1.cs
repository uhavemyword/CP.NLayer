// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 02/21/2013 13:13:53
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Common
{
    using CP.NLayer.Common;
    using CP.NLayer.Models;
    using CP.NLayer.Resources.UI;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.Prism.Regions;
    using Microsoft.Practices.Unity;
    using System;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Windows.Input;

    public class EditViewModel<T> : ViewModelBase, IRegionMemberLifetime, INavigationAware, IDialog
        where T : EditModelBase
    {
        protected IEditModelService<T> _service
        {
            get { return Container.Resolve<IEditModelService<T>>(); }
        }

        protected IInteractionService _interaction
        {
            get { return Container.Resolve<IInteractionService>(); }
        }

        public EditViewModel()
        {
            //if (WpfHelper.GetIsInDesignMode())
            //{
            //    return;
            //}

            base.BusyModel = new RadBusyModel();
            this.SaveCommand = new DelegateCommand<object>(ExecuteSaveCommand, CanExecuteSaveCommand);
            this.CancelCommand = new DelegateCommand(ExecuteCancelCommand, CanExecuteCancelCommand);
        }

        public virtual void Initialize(long id, bool isSaveAndContinueMode)
        {
            this.Id = id;
            this.BusyModel.DoWorkAsync(
                () =>
                {
                    this.Item = (id <= 0 ? _service.Create() : _service.GetById(id));
                }
            );
        }

        private T _item;

        public T Item
        {
            get { return _item; }
            set
            {
                _item = value;
                this.OnPropertyChanged(() => this.Item);
            }
        }

        private long _id;

        public long Id
        {
            get { return _id; }
            set
            {
                _id = value;
                this.OnPropertyChanged(() => this.Id);
                this.OnPropertyChanged((() => this.IsEditMode));
                this.OnPropertyChanged(() => this.Operation);
                this.OnPropertyChanged(() => this.HeaderText);
            }
        }

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
                    //PublishDialogResultEvent();
                }
            }
        }

        private string _headerText;

        public virtual string HeaderText
        {
            get
            {
                _headerText = string.Empty;
                switch (Operation)
                {
                    case OperationEnum.Add:
                        _headerText = UiResources.Add;
                        break;

                    case OperationEnum.Edit:
                        _headerText = UiResources.Edit;
                        break;
                }
                return string.IsNullOrEmpty(ModelName)
                    ? _headerText
                    : string.Format("{1} - {0}", _headerText, ModelName);
            }
            set
            {
                _headerText = value;
                this.OnPropertyChanged(() => this.HeaderText);
            }
        }

        public void OnShow(object arg)
        {
            long id = arg == null ? 0 : (long)arg;
            this.Initialize(id, false);
        }

        public void OnClosing()
        {
            this.DialogResult = false;

            //_interaction.ShowConfirmation(UiResources.CancelPrompt, confirmed =>
            //    {
            //        if (confirmed)
            //        {
            //            this.DialogResult = false;
            //        }
            //        else
            //        {
            //            //do nothing, keep dialog open.
            //        }
            //    });
        }

        //public virtual void PublishDialogResultEvent()
        //{
        //    var eventAggregator = ServiceLocator.Current.GetInstance<IEventAggregator>();
        //    var payload = new DialogResultEventPayload() { Result = this.DialogResult.Value, Subject = this.GetType().FullName };
        //    eventAggregator.GetEvent<DialogResultEvent>().Publish(payload);
        //}

        #endregion

        public string ModelName { get; set; }

        public bool IsEditMode
        {
            get { return Id > 0; }
        }

        public OperationEnum Operation
        {
            get { return Id <= 0 ? OperationEnum.Add : OperationEnum.Edit; }
        }

        public ICommand SaveCommand { get; set; }

        public ICommand CancelCommand { get; set; }

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
            string arg = navigationContext.Parameters["id"];
            long id = arg == null ? 0 : int.Parse(arg);
            this.Initialize(id, false);
        }

        #endregion INavigationAware

        #region IRegionMemberLifetime

        public virtual bool KeepAlive
        {
            get { return false; }
        }

        #endregion IRegionMemberLifetime

        protected virtual bool CanExecuteSaveCommand(object isSaveAndContinueMode)
        {
            return true;
        }

        protected virtual void ExecuteSaveCommand(object isSaveAndContinueMode)
        {
            bool exit = false;
            BusyModel.DoWorkAsync(
                () =>
                {
                    this.ValidationResults = new ObservableCollection<ValidationResult>(this.Item.GetValidationResults());
                    if (this.ValidationResults.Count > 0)
                    {
                        exit = true;
                        return;
                    }

                    try
                    {
                        switch (Operation)
                        {
                            case OperationEnum.Add:
                                Item = _service.Insert(Item);
                                break;

                            case OperationEnum.Edit:
                                _service.Update(Item);
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        this.ValidationResults.Add(new ValidationResult(ex.MostInnerException().Message));
                        this.OnPropertyChanged(() => this.ValidationResults);
                        exit = true;
                        return;
                    }
                },
                () =>
                {
                    if (!exit)
                    {
                        if ((bool)isSaveAndContinueMode)
                        {
                            _interaction.ShowMessage(UiResources.Message, UiResources.SavedSuccessfully, 2);
                            this.Initialize(this.Id, true);
                        }
                        else
                        {
                            this.DialogResult = true;
                        }
                    }
                }
            );
        }

        protected virtual bool CanExecuteCancelCommand()
        {
            return true;
        }

        protected virtual void ExecuteCancelCommand()
        {
            OnClosing();
        }
    }
}