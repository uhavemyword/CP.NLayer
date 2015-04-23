// ------------------------------------------------------------------------------------
//      Copyright (c) uhavemyword(at)gmail.com. All rights reserved.
//      Created by Ben at 2/20/2013 4:01:33 PM
// ------------------------------------------------------------------------------------

namespace CP.NLayer.Models.Business
{
    using CP.NLayer.Common;
    using CP.NLayer.Resources.Model;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    [DataContract(IsReference = true)]
    public class UserDisplayModel : DisplayModelBase
    {
        private string _roleNames;

        [DataMember]
        public long Id { get; set; }

        [DataMember]
        [Display(Name = MResourceNames.UserName, ResourceType = typeof(MResources))]
        public string UserName { get; set; }

        [DataMember]
        [Display(Name = MResourceNames.User_FullName, ResourceType = typeof(MResources))]
        public string FullName { get; set; }

        [DataMember]
        [Display(Name = MResourceNames.Department, ResourceType = typeof(MResources))]
        public string Department { get; set; }

        [DataMember]
        [Display(Name = MResourceNames.Role_Name, ResourceType = typeof(MResources))]
        public string RoleNames
        {
            get
            {
                return _roleNames.Truncate();
            }
            set
            {
                _roleNames = value;
            }
        }

        [DataMember]
        [Display(Name = MResourceNames.IsActive, ResourceType = typeof(MResources))]
        public bool IsActive { get; set; }

        public override string GetDisplayName()
        {
            return this.UserName;
        }
    }
}