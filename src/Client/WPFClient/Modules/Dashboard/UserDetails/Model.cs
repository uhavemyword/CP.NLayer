using CP.NLayer.Models;
using CP.NLayer.Models.Entities;
using CP.NLayer.Resources.Model;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CP.NLayer.Client.WpfClient.Modules.Dashboard.UserDetails
{
    public class Model
    {
        public Model()
        {
        }

        public Model(User item)
        {
            this.Id = item.Id;
            this.UserName = item.UserName;
            this.FullName = item.FullName;
            this.Department = item.Department == null ? string.Empty : item.Department.Name;
            this.IsActive = item.IsActive;
        }

        [Browsable(false)]
        public long Id { get; set; }

        [Display(Order = 1, Name = MResourceNames.UserName, ResourceType = typeof(MResources))]
        public string UserName { get; set; }

        [Display(Order = 2, Name = MResourceNames.User_FullName, ResourceType = typeof(MResources))]
        public string FullName { get; set; }

        [Display(Order = 3, Name = MResourceNames.Department, ResourceType = typeof(MResources))]
        public string Department { get; set; }

        [Display(Order = 4, Name = MResourceNames.IsActive, ResourceType = typeof(MResources))]
        public bool IsActive { get; set; }
    }
}