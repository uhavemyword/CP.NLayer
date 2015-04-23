using CP.NLayer.Client.WpfClient.Common;
using CP.NLayer.Models.Business.Dashboard;
using CP.NLayer.Resources.Model;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.Department
{
    public class ViewModel : ViewModelBase
    {
        private Color _defaultColor = (Color)Application.Current.FindResource("DefaultShapeColor");

        public ViewModel()
        {
            if (WpfHelper.GetIsInDesignMode())
            {
                return;
            }

            LoadData();
        }

        private List<DepartmentModel> _departments;
        public List<DepartmentModel> Departments
        {
            get { return _departments; }
            set
            {
                if (!object.Equals(_departments, value))
                {
                    _departments = value;
                    this.OnPropertyChanged(() => this.Departments);
                }
            }
        }

        private List<DepartmentModel> _selectedDepartments;
        public List<DepartmentModel> SelectedDepartments
        {
            get { return _selectedDepartments; }
            set
            {
                if (!object.Equals(_selectedDepartments, value))
                {
                    _selectedDepartments = value;
                    this.OnPropertyChanged(() => this.SelectedDepartments);
                    ChangeShapesColor();
                }
            }
        }

        private void ChangeShapesColor()
        {
            var dic = new Dictionary<CP.NLayer.Models.Entities.User, Color>();
            foreach (var item in Dashboard.Home.ViewModel.Users)
            {
                if (item.Department != null && this.SelectedDepartments.Any(x => x.Name == item.Department.Name))
                {
                    var color = (Color)ColorConverter.ConvertFromString(this.SelectedDepartments.First(x => x.Name == item.Department.Name).ColorHexValue);
                    dic.Add(item, color);
                }
                else
                {
                    dic.Add(item, _defaultColor);
                }
            }

            var payload = new UsersColorModel()
            {
                Subject = MResources.Department,
                UsersColorDic = dic
            };
            Dashboard.Home.ViewModel.PublishUsersColorChangedEvent(payload);
        }

        private void LoadData()
        {
            var departments = new List<DepartmentModel>();
            foreach (var item in Dashboard.Home.ViewModel.Users)
            {
                if (item.Department != null && !departments.Any(x => x.Name == item.Department.Name))
                {
                    departments.Add(new DepartmentModel() { Name = item.Department.Name });
                }
            }

            if (departments.Count > 0)
            {
                var colors = ColorHelper.GenerateColors(departments.Count);
                for (int i = 0; i < departments.Count; i++)
                {
                    departments[i].ColorHexValue = colors[i].ToString();
                }
            }
            this.Departments = departments;
            this.SelectedDepartments = new List<DepartmentModel>();
        }
    }
}