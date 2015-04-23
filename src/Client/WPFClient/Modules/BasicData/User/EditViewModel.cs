// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com All rights reserved.
//      Created by Ben at 5/12/2014 8:23:23 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Client.WpfClient.Modules.BasicData.User
{
    using CP.NLayer.Client.WpfClient.Common;
    using CP.NLayer.Models.Business;
    using CP.NLayer.Service.Contracts;
    using Microsoft.Practices.Prism.Commands;
    using Microsoft.Practices.ServiceLocation;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Windows.Input;
    using basic = CP.NLayer.Client.WpfClient.Modules.BasicData;

    public class EditViewModel : EditViewModel<UserEditModel>
    {
        public EditViewModel()
        {
            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }

            this.ShowDepartmentsCommand = new DelegateCommand(ExecuteShowDepartmentsCommand);
            this.RefreshDepartmentListAsync();
        }

        public override void Initialize(long id, bool isSaveAndContinueMode)
        {
            this.Id = id;
            this.BusyModel.DoWorkAsync(
                () =>
                {
                    this.Item = (this.Id <= 0 ? _service.Create() : _service.GetById(this.Id));
                    this.SelectedDepartment = null;
                },
                () =>
                {
                    if (this.Operation == OperationEnum.Edit)
                    {
                        this.BusyModel.Await(); //RefreshDepartmentListAsync
                        this.SelectedDepartment = this.DepartmentList.FirstOrDefault(x => x.Target.Id == this.Item.Target.DepartmentId);
                    }
                },
                true
            );
        }

        #region Department

        private DepartmentDisplayModel _selectedDepartment;
        public DepartmentDisplayModel SelectedDepartment
        {
            get { return _selectedDepartment; }
            set
            {
                if (!object.Equals(_selectedDepartment, value))
                {
                    _selectedDepartment = value;
                    this.Item.Target.Department = _selectedDepartment == null ? null : _selectedDepartment.Target;
                    this.OnPropertyChanged(() => this.SelectedDepartment);
                }
            }
        }

        private ObservableCollection<DepartmentDisplayModel> _departmentList;
        public ObservableCollection<DepartmentDisplayModel> DepartmentList
        {
            get { return _departmentList; }
            set
            {
                _departmentList = value;
                this.OnPropertyChanged(() => this.DepartmentList);
            }
        }

        public void RefreshDepartmentListAsync()
        {
            this.BusyModel.DoWorkAsync(() =>
            {
                var service = ServiceLocator.Current.GetInstance<IDepartmentDisplayModelService>();
                this.DepartmentList = new ObservableCollection<DepartmentDisplayModel>(service.GetAll());
            });
        }

        public ICommand ShowDepartmentsCommand { get; set; }

        public void ExecuteShowDepartmentsCommand()
        {
            this._interaction.ShowView(typeof(basic.Department.DisplayView), true, null, dialogResult => this.RefreshDepartmentListAsync());
        }

        #endregion
    }
}